package psu.utils;

import javafx.application.Platform;
import javafx.scene.control.Label;

import java.util.TimerTask;

import static psu.server.ServerController.globalTimer;
import static psu.server.ServerController.globalmatchEnded;

public class CustomTimer extends TimerTask {

    public static int minute;
    public static int second;
    public static boolean isPaused;


    public CustomTimer(int minute, int second) {
        CustomTimer.minute = minute;
        CustomTimer.second = second;
        CustomTimer.isPaused = false;
    }

    @Override
    public void run() {
        Platform.runLater(() -> {
            if (!isPaused){
                if (second > 0) {
                    second--;
                } else if(minute > 0){
                    second = 59;
                    minute--;
                }

                if (minute == 0 && second == 0) {
                    globalTimer.setText(String.format("%02d", minute) + ":" + String.format("%02d", second));
                    return;
                }

                globalTimer.setText(String.format("%02d", minute) + ":" + String.format("%02d", second));
            }
        });
    }

}
