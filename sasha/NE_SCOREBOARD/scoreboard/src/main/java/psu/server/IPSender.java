package psu.server;

import psu.utils.GlobalConstants;

import java.io.IOException;
import java.net.*;

public class IPSender extends Thread {

    private DatagramSocket socket;
    private String localServerIP;

    //в начале общения клиент-сервер. Сервер раздает свой IP
    IPSender() throws SocketException {
        try {
            socket = new DatagramSocket(GlobalConstants.PORT); //создается сокет
            socket.connect(InetAddress.getByName("8.8.8.8"), GlobalConstants.PORT); // на гугл отправляют пакет, из ответа получаем свой ИП
            localServerIP = socket.getLocalAddress().getHostAddress();
            socket.disconnect();
            this.setDaemon(true); //при выключении сервера, выключается программа
        } catch (UnknownHostException ex) {
            ex.printStackTrace();
            //TODO нормально обработать
        }
    }

    @Override
    //слушатель, говорит свой ИП
    public void run() {
        DatagramPacket packet;
        byte[] buf;
        String received;

        try {
            while (true) {
                buf = new byte[GlobalConstants.BUF_SIZE_IP];
                packet = new DatagramPacket(buf, buf.length);
                socket.receive(packet); //тут стоит, ждет сообщений от клиентов
                received = new String(packet.getData(), 0, packet.getLength()); //имя пользователя

                if (received.equals(GlobalConstants.GET_SERVER_IP)) {
                    buf = localServerIP.getBytes();
                    packet = new DatagramPacket(
                            buf,
                            buf.length,
                            packet.getAddress(),
                            packet.getPort());

                    socket.send(packet);
                }
            }
        } catch (IOException ex) {
            ex.printStackTrace();
            //TODO нормально обработать
        } finally {
            socket.close();
        }
    }
}
