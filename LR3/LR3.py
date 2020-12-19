import datetime
import os
import time

#Импорт библиотек


b = datetime.time(10,25)#Задаем время для запуска блокнота
time_point = True#Переменная для выхода из цикла

while time_point:#Он выйдет из цикла как только откроет блокнот
    time.sleep(20)#жидание 20 секунд, что бы цикл не так часто проверял
    time_now = datetime.datetime.now().time()#Берем текущее время
    if b.hour == time_now.hour and time_now.minute==b.minute:#сравниваем текущее время с заданным
        time_point = False 
        os.system("notepad")#Открываем блокнот
