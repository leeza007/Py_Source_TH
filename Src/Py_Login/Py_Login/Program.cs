using PangyaAPI;
using PangyaAPI.PangyaClient;
using PangyaAPI.PangyaPacket;
using Py_Login.MainServer;
using System;

namespace Py_Login
{
    public class Program
    {
        #region Fields
        public static LoginServer Server;
        #endregion

        static void Main()
        {
            Console.Title = $"Pangya Fresh UP ! LoginServer";

            Server = new LoginServer();

            Server.ServerStart();

            Server.OnPacketReceived += Server_OnPacketReceived;
            //Escuta contínuamente entradas no console (Criar comandos para o Console)
            for (;;)
            {
                var comando = Console.ReadLine().Split(new char[] { ' ' }, 2);
                
				Server.RunCommand(comando);
            }
        }

        private static void Server_OnPacketReceived(Player player, Packet packet)
        {
            var Client = (LPlayer)player;

            Client.HandleRequestPacket((PangyaPacketsEnum)packet.Id, packet);
        }
    }
}
