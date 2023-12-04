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
        [FormerlySerializedAs("Animator")] public Animator animator;

        /*
         * Test
         */
        private UdpClient udpClient;

        void Start()
        {
            /*
         * Connection test UDP
         */
            var serverInfo = new ServerInfo();
            udpClient = new UdpClient(serverInfo.GetHost(), serverInfo.GetPortUDP());
        }

        void Update()
        {
            Forward();
            Backward();
            Leftward();
            Rightward();
        }

        public void SendPosition()
        {
            var writer = new WritePacket();
            writer.Write((int)OpcodePackets.PLAYER_LOCALIZATION);
            writer.Write(transform.position.x);
            writer.Write(transform.position.y);
            writer.Write(transform.position.z);

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
            /*if (Input.GetKey(KeyCode.A))
            {
                animator.SetBool("isWalking", true);
                transform.Translate(Vector3.left * Time.deltaTime);
                
                SendPosition();
            }
            else
            {
                animator.SetBool("isWalking", false);
            }*/
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