using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof(Camera))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;

        public WeaponFire playersWeapon;
        public ThrowThing playersGrenade;

        public Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_NextStep;
        private bool m_Jumping;

        private float speedTrack;

        [Header("Movement Variables: ")]
        [SerializeField][Range(0,1000)]
        private float friction;
        [SerializeField][Range(0, 1000)]
        private float ground_accelerate;
        [SerializeField][Range(0, 1000)]
        private float air_accelerate;
        [SerializeField][Range(0, 1000)]
        private float max_velocity_ground;
        [SerializeField][Range(0, 1000)]
        private float max_velocity_air;

        // Use this for initialization
        private void Start()
        {


            m_CharacterController = GetComponent<CharacterController>();
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_Jumping = false;
			m_MouseLook.Init(transform , m_Camera.transform);

        }


        // Update is called once per frame
        private void Update()
        {

            GetInput();

            RotateView();
                // the jump state needs to read here to make sure it is not missed
                if (!m_Jump && m_CharacterController.isGrounded)
                {
                    m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
                }

                if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
                {
                    m_MoveDir.y = 0f;
                    m_Jumping = false;
                }
                if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
                {
                    m_MoveDir.y = 0f;
                }

                m_PreviouslyGrounded = m_CharacterController.isGrounded;

            playersWeapon.checkForFiring();
            playersGrenade.throwThing();
            m_MouseLook.UpdateCursorLock();

        }


        private void FixedUpdate()
        {

            //always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;
            desiredMove = desiredMove.normalized;
            if (m_CharacterController.isGrounded)
            {

                // get a normal for the surface that is being touched to move along it
                //RaycastHit hitInfo;
                //Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                              // m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
                //desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;


                m_MoveDir = MoveGround(desiredMove, m_CharacterController.velocity);

                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                m_MoveDir = MoveAir(desiredMove, m_CharacterController.velocity);
                m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
            }

            m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

        }

        private void GetInput()
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            
            

            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

        }


        private void RotateView()
        {
            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Acceleration);
        }

        // accelDir: normalized direction that the player has requested to move (taking into account the movement keys and look direction)
        // prevVelocity: The current velocity of the player, before any additional calculations
        // accelerate: The server-defined player acceleration value
        // max_velocity: The server-defined maximum player velocity (this is not strictly adhered to due to strafejumping)
        private Vector3 Accelerate(Vector3 accelDir, Vector3 prevVelocity, float accelerate, float max_velocity)
        {
            float projVel = Vector3.Dot(prevVelocity, accelDir); // Vector projection of Current velocity onto accelDir.
            float accelVel = accelerate * Time.fixedDeltaTime; // Accelerated velocity in direction of movment

            // If necessary, truncate the accelerated velocity so the vector projection does not exceed max_velocity
            if (projVel + accelVel > max_velocity)
                accelVel = max_velocity - projVel;

            return prevVelocity + accelDir * accelVel;
        }

        private Vector3 MoveGround(Vector3 accelDir, Vector3 prevVelocity)
        {
            // Apply Friction
            float speed = prevVelocity.magnitude;
            if (speed != 0) // To avoid divide by zero errors
            {
                float drop = speed * friction * Time.fixedDeltaTime;
                prevVelocity *= Mathf.Max(speed - drop, 0) / speed; // Scale the velocity based on friction.
            }

            // ground_accelerate and max_velocity_ground are server-defined movement variables
            return Accelerate(accelDir, prevVelocity, ground_accelerate, max_velocity_ground);
        }

        private Vector3 MoveAir(Vector3 accelDir, Vector3 prevVelocity)
        {
            // air_accelerate and max_velocity_air are server-defined movement variables
            return Accelerate(accelDir, prevVelocity, air_accelerate, max_velocity_air);
        }


    }
}

