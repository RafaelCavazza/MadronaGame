using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public GameObject LastBackgound;
        public float damping = 0;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;
        public bool OnlyUpCamera = false;
        public float BackgroundHeigthSensitvity = 4;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

        // Use this for initialization
        private void Start()
        {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }


        // Update is called once per frame
        private void Update()
        {
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
                m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
            else
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            newPos.x = transform.position.x;
            CreatNewBackGround();
            if (OnlyUpCamera)
            {
                if (newPos.y > transform.position.y)
                {
                    SetWallHeigth(newPos.y - transform.position.y);
                    transform.position = newPos;
                    m_LastTargetPosition = target.position;
                }
            }
            else
            {
                SetWallHeigth(newPos.y - transform.position.y);
                transform.position = newPos;
                m_LastTargetPosition = target.position;
            }
        }

        void SetWallHeigth(float size)
        {
            var box = GameObject.FindGameObjectsWithTag("Box");
            foreach (var gob in box)
            {
                var coliders = gob.GetComponents<BoxCollider2D>();
                foreach (var col in coliders)
                {
                    if (col.isTrigger == false)
                    {
                        col.size = new Vector2(col.size.x, col.size.y + (size * 2));
                    }
                }
            }
        }

        /// <summary>
        /// <para> cria um novo BackGround se estiver na altura certa. </para>
        /// </summary>
        void CreatNewBackGround()
        {
            if (LastBackgound == null)
                return;

            if (isMaxHeight())
            {
                var new_LastBackgound = GameObject.Instantiate(LastBackgound);
                var transform = new_LastBackgound.GetComponent<Transform>();
                var sprite = new_LastBackgound.GetComponent<SpriteRenderer>();

                transform.position = new Vector2(transform.position.x, transform.position.y + (sprite.bounds.extents.y * 2));

                new_LastBackgound.name = SetNameNewBackGround();
                this.LastBackgound = new_LastBackgound;
            }
        }

        /// <summary>
        /// <para> Define o nome para novo BackGround </para>
        /// </summary>
        /// <returns></returns>
        private string SetNameNewBackGround()
        {
             var numberStr = LastBackgound.name.Split('_')[1];
             int number = int.Parse(numberStr) + 1;
             string new_name = "BackGroundSky_" + number.ToString();
             return new_name;
        }

        /// <summary>
        /// <para> Calcula se está na altura correta para criar mais um background. </para>
        /// </summary>
        /// <returns></returns>
        private bool isMaxHeight()
        {
            var currentHeight = this.transform.position.y;
            var LastBackgoundHeight = LastBackgound.GetComponent<Transform>().position.y;
            return (currentHeight + BackgroundHeigthSensitvity) > LastBackgoundHeight;
        }
    }
}
