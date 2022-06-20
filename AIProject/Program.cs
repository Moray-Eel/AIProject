using AIProject.Classes;

//Inicjalizacja sieci przebiega według następujących instrukcji
//Piwerszy argument to tablica intów, w której przechowywane są ilości neuronów w każdej warstwie.
//Długość tablicy odpowiada za ilosc naszych warstw -> {5,3,3,2} oznacza, że sieć będzie miałą 4 warstwy 
//Z czego w pierwszej wartswie bedzie 5 neuronow, w dwoch warstwach ukrytych będą po 3 neurony i w warstwie wyjsciowej beda 2 neurony 
//Drugi argument to tablica stringow z czego 1 element tej tablicy oznacza pliki z wejsciami a drugi pliki z wartosciami docelowymi
//Pliki moga byc w formacie tsv lub csv. Ilosci neuronow w 1 i ostatniej warstwie nie sa obliczane na podstawie wcytanych danych z pliku, tzn. jesli
//mamy plik wejsciowy z 5 kolumnami danych to w 1 argumencie trzeba podac liczbe 5, tak samo jesli mamy 2 kolumny w pliku docelowym to trzeba podac w warstwie
//wyjsciowej liczbe 2. Warstwy ukryte są tworzeone dynamicznie i można je modyfikować dowolnie


NeuralNetwork neuralNetwork = new(new int[] { 4,3,1 }, new string[] { @"C:\Users\slikm\Pulpit\xd.tsv", @"C:\Users\slikm\Pulpit\IrisTarget1.tsv" });
neuralNetwork.Run();
Console.ReadKey();


