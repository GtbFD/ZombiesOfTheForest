using System.Net;
using System.Net.Sockets;
using Interfaces;
using packets.enums;
using server.utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace game
{
    public class PlayerMovement : MonoBehaviour, IMovement
    {
        [FormerlySerializedAs("Animator")] 
        private Animator animator;

        private Vector2 inputPlayer;

        /*
         * Test
         */
        private UdpClient udpClient;

        private PlayerData playerData;
        private IPEndPoint ipEndPoint;
        

        void Start()
        {
            /*
         * Connection test UDP
         */
            playerData = new PlayerData();
            
            var serverInfo = new ServerInfo();
            udpClient = new UdpClient(serverInfo.GetHost(), serverInfo.GetPortUDP());

            ipEndPoint = new IPEndPoint(IPAddress.Any, 0);

            animator = GameObject.FindObjectOfType<Animator>();
        }

        void Update()
        {
            /*Forward();
            Backward();
            Leftward();
            Rightward();*/

            inputPlayer = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            animator.SetFloat("x", inputPlayer.x);
            animator.SetFloat("y", inputPlayer.y);
            t
        }

        public void SendPosition()
        {
            PlayerData.x = transform.position.x;
            PlayerData.y = transform.position.y;
            PlayerData.z = transform.position.z;
            
            var writer = new WritePacket();
            writer.Write((int)OpcodePackets.PLAYER_LOCALIZATION);
            writer.Write(PlayerData.x);
            writer.Write(PlayerData.y);
            writer.Write(PlayerData.z);

            var playerLocalizationPacket = writer.BuildPacket();

            //ConnectionPlayer.GetInstance().GetConnection().Send(playerLocalizationPacket);

            udpClient.Send(playerLocalizationPacket, playerLocalizationPacket.Length);

        }

        public void Forward()
        {
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool("isForward", true);
                transform.Translate(Vector3.forward * Time.deltaTime);

                SendPosition();
            }
            else
            {
                animator.SetBool("isForward", false);
            }
        }

        public void Backward()
        {
            if (Input.GetKey(KeyCode.S))
            {
                animator.SetBool("isBackward", true);
                transform.Translate(Vector3.back * Time.deltaTime);

                SendPosition();
            }
            else
            {
                animator.SetBool("isBackward", false);
            }
        }

        public void Leftward()
        {
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetBool("isWalkingLeft", true);
                transform.Translate(Vector3.left * Time.deltaTime);
                
                SendPosition();
            }
            else
            {
                animator.SetBool("isWalkingLeft", false);
            }
        }

        public void Rightward()
        {
            /*if (Input.GetKey(KeyCode.D))
            {
                animator.SetBool("isWalking", true);
                transform.Translate(Vector3.right * Time.deltaTime);
                
                SendPosition();
            }
            else
            {
                animator.SetBool("isWalking", false);
            }*/
        }
    }
}