package com.company;

import java.io.*;

public class Main {

    public static void main(String[] args) throws InterruptedException {

        while(true) {
            Thread.sleep(2000);
            try (FileWriter writer = new FileWriter("C://Users/Дмитрий/Desktop/Мои работы/Файлы приходят/log.txt", true)) {
                // запись всей строки
                String text = "Выявлена ошибка номер "+Math.random()+"\n";
                writer.write(text);
                // запись по символам
            } catch (IOException ex) {

                System.out.println(ex.getMessage());
            }

        }
    }
}
