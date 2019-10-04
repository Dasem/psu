package psu.server;

import psu.entities.ConnectionResult;

import java.io.*;
import java.net.ServerSocket;
import java.net.Socket;

import static psu.utils.GlobalConstants.PORT;

public class Server {

    public static void main(String[] args) {

        try (ServerSocket serverSocket = new ServerSocket(PORT)) {
            IPSender ipSender = new IPSender();
            ipSender.start(); //слушатель запускается
            while (true) {
                Socket accept = serverSocket.accept();
                UserConnection userConnection = new UserConnection(accept);  //ставит мой порт на прослушивание, если что возвращает обычный сокет
                if (userConnection.getConnectionResult() == ConnectionResult.SUCCESS){
                    new Thread(userConnection).start();
                }
            }
        } catch (IOException | ClassNotFoundException e) {
            e.printStackTrace();

        }
    }
}
