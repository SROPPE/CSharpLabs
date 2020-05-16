namespace LB3_C_SHARP
{
    interface ICar
    {
        void Beep();
        string PlayMusic(string nameOfSong);//Включает выбранную песню
        string UseWipers();//Активирует работу дворников
        string OpenWindow(int num);//Открывает окно
        void ShowCarDashboard();//Отражает панель приборов

    }
}
