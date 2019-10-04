package psu.client;

import javafx.application.Platform;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.scene.control.*;
import javafx.scene.layout.Pane;
import javafx.stage.DirectoryChooser;
import javafx.stage.FileChooser;
import psu.entities.Message;
import psu.entities.MessageType;
import psu.utils.FileSender;
import psu.utils.GlobalConstants;
import psu.utils.Utils;

import java.io.File;
import java.io.IOException;
import java.text.MessageFormat;
import java.util.*;

import static psu.utils.Utils.*;

public class FileExporterClientController {

    public static String destinationFolder = "C:/";

    public static Object selectedUser;

    public static File openedFile;

    public static File openedFolder;

    public static ObservableList<String> connectedUsers;


    @FXML
    private Pane root;

    @FXML
    private TextArea textArea;

    @FXML
    private Button RefuseFile;

    @FXML
    private Label lblAcceptFile;

    @FXML
    private Button btnfileFolder;

    @FXML
    private ListView usersList;

    @FXML
    private Label fileSize;

    @FXML
    private TextField userMessageField;

    @FXML
    private Label SomeOnePrinting;

    @FXML
    private void sendMessage() {
        //гет - возвращает поток клиент сообщ работник
        //есть работник, он давно создан, потом просто его получаем
        ClientMessageWorker.getInstance().sendMessage(userMessageField.getText());
        userMessageField.clear();
    }

    @FXML
    private void sendFile() {
        //selectedUser = usersList.getSelectionModel().getSelectedItem();

        if (selectedUser == null) {
            showAlertMessage("Sending File", "", "Choose accepter from user list", Alert.AlertType.ERROR);
            return;
        }

        if (openedFile == null) {
            showAlertMessage("Sending File", "", "File not chosen", Alert.AlertType.ERROR);
            return;
        }

        ClientMessageWorker.getInstance().sendFileNotification();
        FileSender.sendFile(openedFile);
    }

    public FileExporterClientController() {
        connectedUsers = FXCollections.observableArrayList();
        usersList = new ListView<String>();
        usersList.getSelectionModel().setSelectionMode(SelectionMode.SINGLE);
        usersList.setItems(connectedUsers);
        ClientMessageWorker.getInstance().setController(this);
    }

    @FXML
    //отправить
    private void openFileDialog() {
        selectedUser = usersList.getSelectionModel().getSelectedItem();
        if (selectedUser == null) {
            showAlertMessage("Ошибка", "", "Пользователь не выбран!", Alert.AlertType.ERROR);
        }
        else{
            FileChooser fileChooser = new FileChooser();
            fileChooser.setTitle("Choose file");
            openedFile = fileChooser.showOpenDialog(root.getScene().getWindow());
            if (openedFile != null) {
                fileSize.setText(MessageFormat.format(GlobalConstants.FILE_SIZE_PATTERN, getFileSize(openedFile)));
//            filePathTextField.setText(openedFile.getAbsolutePath());

                sendRequestAcceptFile();
                //
            }
        }
    }


    @FXML
    //Принять
    private void fileFolder() {
        DirectoryChooser directoryChooser = new DirectoryChooser();
        directoryChooser.setTitle("Choose directory for download");
        openedFolder = directoryChooser.showDialog(root.getScene().getWindow());
        if (openedFolder != null) {
            destinationFolder = openedFolder.getAbsolutePath();
            System.out.println("destination folder: " + destinationFolder);
            //Utils.showAlertMessage("Accepted files", "Файл успешно загружен!", "Путь: ''" + destinationFolder + "''", Alert.AlertType.INFORMATION);
            RefuseAcceptFile();
            sendConformationUploadFile(selectedUser.toString());
        }
    }

    @FXML
    private void PrintText() {
        sendMessageAboutPrinting();
    }

    @FXML
    private void RefuseAcceptFile() {
        Platform.runLater(() -> {
            RefuseFile.setVisible(false);
            btnfileFolder.setVisible(false);
            lblAcceptFile.setText("");
        });
    }


    public synchronized void pushToTextArea(String sender, String message) {
        textArea.appendText(sender + ": " + message + "\n");
    }

    public synchronized void ShowButtonsAboutFile(String Sender) {
      //  openedFile = pathFile;
        selectedUser = Sender;

        Platform.runLater(() -> {
            RefuseFile.setVisible(true);
            btnfileFolder.setVisible(true);
            lblAcceptFile.setText("Пришел файл от "+ Sender);
        });
    }

    public synchronized void setUsersList(List<String> users) {

        List<String> usersWithoutMe = new ArrayList<>();
        String currentUser = ClientMessageWorker.getInstance().getClientName();
        for (String user:users){
            if (!currentUser.equals(user)){
                usersWithoutMe.add(user);
            }
        }
        Platform.runLater(() -> connectedUsers.setAll(usersWithoutMe));
        usersList.setItems(connectedUsers);
    }

    private synchronized void sendRequestAcceptFile() {

        ClientMessageWorker.getInstance().sendRequestAcceptFile(selectedUser.toString());
        userMessageField.clear();
    }

    private synchronized void sendMessageAboutPrinting() {
        ClientMessageWorker.getInstance().sendMessageAboutPrinting();
    }

    private synchronized void sendConformationUploadFile(String UserRec) {
        ClientMessageWorker.getInstance().sendConformationUploadFile(UserRec);
    }

    public synchronized void printThatSomeOnePrintig(String Printer) {

        Platform.runLater(() -> { //лямбда что-то
            SomeOnePrinting.setText(MessageFormat.format(GlobalConstants.MESSAGE_PRINTING, Printer));
        });

        new Thread(()->{
            try {
                Thread.sleep(2000);
                Platform.runLater(() -> { //лямбда что-тo
                    SomeOnePrinting.setText("");
                });
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }).start();

    }

    public synchronized void sendFileForUser(String recipient) {
        selectedUser = recipient;
        sendFile();
    }

}
