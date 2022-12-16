using System.IO;
using System.Data.Common;


//input number
int Input()
{
    int n;
    while (true)
    {
        Console.WriteLine("Įveskite atitinkamą skaičių:");
        string input = Console.ReadLine();
        bool isNumber = int.TryParse(input, out n);
        if (isNumber)
        {
            return n;

        }
        else
        {
            Console.WriteLine("Klaida!!!");
        }
    }
}


//input letter or word
string Letter()
{
    char n;
    string input;
    int m;
    while (true)
    {
        Console.WriteLine("Įveskite raidę (arba visą žodį):");
        input = Console.ReadLine();
        bool isChar = char.TryParse(input, out n);
        if (isChar)
        {
            return Char.ToString(n);

        }
        else
        {
            Console.WriteLine("Ar tai buvo ivestas pilnas žodis?");
            Console.WriteLine("Įveskite 1 jejgu taip arba bet ką kitą jeigu ne:");
            string num = Console.ReadLine();
            bool isNumber = int.TryParse(num, out m);
            Space();
            if ((isNumber) && (m == 1))
            {
                return input;
            }
        }
    }
}


//painting
void Graphic(int mistake){
    Console.WriteLine("         _______");
    Console.WriteLine("         |     |");
    Console.WriteLine("         |     |");
    switch (mistake)
    {
        case 0:
            Console.WriteLine("               |");
            Console.WriteLine("               |");
            Console.WriteLine("               |");
            Console.WriteLine("               |");
            Console.WriteLine("               |");
            break;
        case 1: 
            Console.WriteLine("         Q     |");
            Console.WriteLine("               |");
            Console.WriteLine("               |");
            Console.WriteLine("               |");
            Console.WriteLine("               |");
            break;
        case 2:
            Console.WriteLine("         Q     |");
            Console.WriteLine("         |     |");
            Console.WriteLine("         |     |");
            Console.WriteLine("               |");
            Console.WriteLine("               |");
            break;
        case 3:
            Console.WriteLine("         Q     |");
            Console.WriteLine("         |\\    |");
            Console.WriteLine("         | `   |");
            Console.WriteLine("               |");
            Console.WriteLine("               |");
            break;
        case 4:
            Console.WriteLine("         Q     |");
            Console.WriteLine("        /|\\    |");
            Console.WriteLine("       ' | `   |");
            Console.WriteLine("               |");
            Console.WriteLine("               |");
            break;
        case 5:
            Console.WriteLine("         Q     |");
            Console.WriteLine("        /|\\    |");
            Console.WriteLine("       ' | `   |");
            Console.WriteLine("          \\    |");
            Console.WriteLine("           \\   |");
            break;
        case 6:
            Console.WriteLine("         Q     |");
            Console.WriteLine("        /|\\    |");
            Console.WriteLine("       ' | `   |");
            Console.WriteLine("        / \\    |");
            Console.WriteLine("       /   \\   |");
            break;
    }
    Console.WriteLine("         ______|_");
}


//Space
void Space()
{
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("");
}


//difficult choise
int Difficult()
{
    while (true)
    {
        Console.WriteLine("Pasirinkite sunkumą.");
        Console.WriteLine("1) Lengvas.");
        Console.WriteLine("2) Vidutinis.");
        Console.WriteLine("3) Sunkus.");
        Space();
        int key = Input();
        if ((key > 0) && (key < 4))
        {
            return key;
        }
        else
        {
            Console.WriteLine("Klaida!!!Nėra tokio pasirinkimo.");
            Console.WriteLine("Pabandykite iš naujo");
        }
    }
}


//game
void Game(int difficalty) 
{
    string[] words = Words(); //array of all words
    List<string> wordsWithDif = new List<string>(); // list of words with needed difficulty
    List<char> usedLetters = new List<char>(); // list of used letters
    string mWord, letterStr;
    bool gameMarker = false;  // marker is game finished or not
    char letter;   
    int mistake = 0;
    foreach (string word in words)
    {
        if ((difficalty == 1) && (word.Length > 5) || (difficalty == 3) && (word.Length < 9) || (difficalty == 2) && ((word.Length < 6) || (word.Length > 8)))
        {
            continue;
        }
        wordsWithDif.Add(word);
    }
    Random rnd = new Random();
    int num = rnd.Next(0, wordsWithDif.Count);
    mWord = wordsWithDif[num]; // word of this game
    Space();
    Space();  // sreen cleaning
    Space();
    Space();
    char[,] closedWord = new char[mWord.Length, 2]; //array with letters of word in 0 and open/closed letters in 1
    for (int i = 0; i < mWord.Length; i++)
    {
        closedWord[i,0] = mWord[i];
        closedWord[i, 1] = '*'; // at start all letters closed
    }
    num = rnd.Next(0, mWord.Length); // first random opened letter
    letter = closedWord[num, 0];
    for (int i = 0; i < mWord.Length; i++) //checking all leters in the word for same first opened letter
    {
        if (closedWord[i, 0] == letter)
        {
            closedWord[i, 1] = letter;
        }
        
    }
    while (true) // second oppened letter
    {
        num = rnd.Next(0, mWord.Length);
        if (closedWord[num, 0] == letter)
        {
            continue;
        }
        else
        {
            letter = closedWord[num, 0];
            for (int i = 0; i < mWord.Length; i++)
            {
                if (closedWord[i, 0] == letter)
                {
                    closedWord[i, 1] = letter;
                }

            }
            break;
        }
    }
    Console.WriteLine("START"); // start of the game
    while (true) // game loop
    {
        if (EndGame(mistake,gameMarker)) // end game check
        {
            break;
        }
        Console.WriteLine();
        Console.WriteLine();
        Graphic(mistake);       //painting Hangman
        Console.Write("                        ");
        for (int i = 0; i < mWord.Length; i++) // writing covered word
        {
            Console.Write(Char.ToUpper(closedWord[i, 1]));
        }
        Console.WriteLine();
        Console.Write("  Panaudotos raidės - "); 
        foreach (char lett in usedLetters)  // used letters
        {
            Console.Write(Char.ToUpper(lett) + " ");
        }
        Console.WriteLine();
        Console.WriteLine("     Klaidos - " + mistake);
        Space();
        letterStr = Letter(); // new letter input
        if (letterStr.Length == 1) // check is this letter or all word
        {
            letter = letterStr[0];
        }
        else
        {
            if (letterStr.Length == mWord.Length) //if word - chjecking is input word is the same
            {
                bool mrk = true;
                for (int i = 0; i < mWord.Length; i++)
                {
                    if (letterStr[i] != closedWord[i, 0]) 
                    { 
                        mrk = false; 
                        break; 
                    }
                }
                if (mrk)
                {
                    gameMarker = true;
                }
                else
                {
                    mistake++;
                }
            }
            else
            {
                mistake++;
            }
            continue;
        }
        Space();
        Console.WriteLine(letter);
        bool marker = false; // checking does input letter exsists in the word
        for (int i = 0; i < mWord.Length; i++)
        {
            if (closedWord[i, 0] == letter)
            {
                marker = true;  
                closedWord[i, 1] = letter;
            }

        }
        if (marker) // exsists
        {
            Space();
            Console.WriteLine("Jūsų spėjimas buvo sėkmingas!");
            Space();
            gameMarker = true;
            for (int i = 0; i < mWord.Length; i++)
            {
                if (closedWord[i, 1] == '*')
                {
                    gameMarker = false;
                }                
            }
        }
        else // not exsist
        {
            mistake++;
            Space();
            Console.WriteLine("Jūsų spėjimas buvo nesėkmingas!");
            usedLetters.Add(letter);
            Space();
        }        
    }
    Console.WriteLine(); // rezults after game
    Console.Write("Žodis buvo - ");
    for (int i = 0; i < mWord.Length; i++)
    {
        Console.Write(Char.ToUpper(closedWord[i, 0]));
    }
    Console.WriteLine();
    Console.WriteLine("Klaidos - " + mistake);
    Console.WriteLine();
    Graphic(mistake);
    Console.WriteLine("Norėdami tęsti, paspauskite Enter.");
    string input = Console.ReadLine();
    Space();
}


//endgame
bool EndGame(int mistake, bool marker)
{
    if ((mistake == 6) || marker)
    {
        if (marker)
        {
            Space();
            Space();
            Console.WriteLine(" Jūs laimėjote!!!");
        }
        else
        {
            Space();
            Space();
            Console.WriteLine(" Jūs pralaimejote :(");
        }
        return true;
    }
    else { return false; }
}

// file/data input
string[] Words()
{
    string wrds = "katinas telefonas vilnius futbolas skandalas schema popierius rytas oras kalba"; // 
    File.WriteAllText("kartuves.txt", wrds); // create file, if file exsists, please comment this line
    string readText = File.ReadAllText("kartuves.txt"); //read file to string
    return readText.Split(' ', ',', '.', ':', '\t'); // return array of words from string
}




//main 
string difficult = "lengvas";
int dif = 1;
int playerKey;
bool marker = false;

while (true)
{
    Space();
    Console.WriteLine("Sveiki! Tai žaidimas 'Kartuvės'.");
    Space();
    Console.WriteLine("1) Naujas žaidimas.");
    Console.WriteLine("2) Keisti žaidimo sunkumą.      Dabartinis - " + difficult + ".");
    Console.WriteLine("0) Išeiti.");
    Space();
    playerKey = Input();
    switch (playerKey) 
    {
        case 0:
            marker = true;
            break;
        case 1:
            Space();
            Game(dif);
            Space();
            break;
        case 2:
            Space();
            dif = Difficult();
            switch (dif)
            {
                case 1: 
                    difficult= "lengvas"; 
                    break;
                case 2: 
                    difficult= "vidutinis";
                    break;
                case 3: 
                    difficult= "sunkus";
                    break;
            }
            Space();
            Console.WriteLine("Dabartinis sunkumas - " + difficult + ".");
            Space();
            break;
        default: 
            Space();
            Console.WriteLine("Tokio pasirinkimo nera.");
            Space();
            break;

    }
    if (marker)
    {
        Space();
        Console.WriteLine("Viso gero!!!");
        Space();
        break;
    }
}