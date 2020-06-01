using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Transactions;
using System.Xml.Schema;

namespace BIT265_MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create new Games List
            Game g = new Game();
            ListEmpty le = new ListEmpty();
            GameList gl = new GameList(g, le);
            List<Game> Games;
            Games = gl.CreateGameList();

            int input = 1;
            while (input != 0)
            {
                Console.WriteLine("Sort your Games by Sell Price!");
                Console.WriteLine("----------------");
                Console.WriteLine();
                Console.WriteLine("Choose from the following options:");
                Console.WriteLine("  1: Add a game");
                Console.WriteLine("  2: View random 10 Games (unsorted) from list");
                Console.WriteLine("  3: View current (unsorted) list");
                Console.WriteLine("  4: View the Current Games List - Ascending Order");
                Console.WriteLine("  5: View the Current Games List - Descending Order");
                Console.WriteLine("  6: Clear your current Games List");
                Console.WriteLine("  7: Create a new Games List");
                Console.WriteLine("  0: Exit application");
                Console.WriteLine("----------------");
                string inp = Console.ReadLine();
                int outc = 6;
                bool check = Int32.TryParse(inp, out outc);
                if (check)
                    input = Int32.Parse(inp);
                else
                    input = 6;  //hard coded, not sure why it sets to 0 atm

                MergeSortAlgorithm ms = new MergeSortAlgorithm(le);
                List<Game> newGamesList = ms.MergeSort(Games);

                switch (input)
                {
                    case 1:
                        {
                            int price;
                            string nameGame;
                            Console.WriteLine("You MAY enter a price for your game, then hit return:");
                            var p = Console.ReadLine();
                            int result = 0;
                            bool outcome = int.TryParse(p, out result);
                            if (outcome)
                                price = Int32.Parse(p);
                            else
                                price = result;
                            Console.WriteLine("You MAY enter a name of a game, then hit return:");
                            nameGame = Console.ReadLine();
                            g.AddNewGame(price, nameGame);
                            Games = gl.ReturnGames();
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("10 games in your list include:");
                            ms.Print10(g, gl);
                            Console.WriteLine("----------------");
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("The current list of {0} games in it's unsorted order", Games.Count);
                            ms.PrintAll(Games, false);
                            Console.WriteLine("----------------");
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Now we'll sort the list of {0} games with MergeSort in ascending order", newGamesList.Count);
                            ms.PrintAll(newGamesList, false);
                            Console.WriteLine("----------------");
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("Now we'll sort the list of {0} games with MergeSort in descending order", newGamesList.Count);
                            ms.PrintAll(newGamesList, true);
                            Console.WriteLine("----------------");
                            break;
                        }
                    case 6:
                        {
                            Console.WriteLine("Are you sure you want to clear your Game List? - Press Y to confirm");
                            string answer = Console.ReadLine();
                            if (answer == "Y" || answer == "y")
                                gl.ClearList();
                            break;
                        }
                    case 7:
                        {
                            gl.CreateGameList();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("You didn't enter in a correct value");
                            break;
                        }
                }
            }
            Console.WriteLine("-------------------");
            Console.WriteLine("End of the Application");
        }

        class ListEmpty 
        {
            public ListEmpty() { }
            public bool listEmpty = true;
            public bool IsListEmpty()
            {
                return listEmpty;
            }
            public void SetListEmpty(bool set)
            {
                listEmpty = set;
            }
        }

        class Game
        {
            int id;
            int price;
            string name;
            int numSold;
            int minGamePrice = 10;
            int maxGamePrice = 79;
            GameList gameList; 
            List<string> gameTitles = new List<string> { "Magic the Gathering", "Pokemon", "Yu-Gi-Oh!", "Keyforge", "Cardfight!! Vanguard", "Dragon Ball Super TCG", "Hearthstone", "Gwent", "Final Fantasy TCG", "Force of Will TCG", "Weiss Schwarz", "Transformers TCG", "GloomHaven", "Super Mario", "Sonic", "Halo", "Fortnight", "League of Legends", "Call of Duty", "Candy Crush", "Fire Emblem", "Legend of Zelda", "Star Wars" };

            Random r = new Random();

            public Game() { }

            public Game(GameList gl)
            {
                gameList = gl;
            }

            public Game(int id, int p, string n, int sold)
            {
                this.id = id;
                this.price = p;
                this.name = n;
                this.numSold = sold;
            }

            public void SetPrice(int p) 
            {
                this.price = p;
            }

            public int ReturnNewPrice() 
            {
                int rPrice = r.Next(minGamePrice, maxGamePrice);
                return rPrice;
            }
            public int ReturnPrice()
            {
                return this.price;
            }

            public void SetName(string name)
            {
                this.name = name;
            }

            public string ReturnNewName() 
            {
                int index = r.Next(0, gameTitles.Count);
                return gameTitles[index];
            }
            public string ReturnName()
            {
                return this.name;
            }

            public int ReturnNewID()
            {
                return gameList.ReturnListSize() + 1;
            }

            public int ReturnID() 
            {
                return this.id;
            }

            public void SetSales(int s) 
            {
                this.numSold = s;
            }
            public int ReturnNewSale() 
            {
                return r.Next(1, 50);
            }

            public int ReturnSales() 
            {
                return this.numSold;
            }


            public void AddNewGame(int price, string gameName)
            {
                int newPrice = price;
                string newName = gameName;
                if (price == 0)
                {
                    newPrice = ReturnNewPrice();
                    Console.WriteLine("An incorrect input was spotted, we'll create a random price for you!");
                }
                if (gameName == "")
                {
                    newName = ReturnNewName();
                    Console.WriteLine("An incorrect input was spotted, we'll create a random game for you!");
                }
                int newId = ReturnNewID();
                int newSales = ReturnNewSale();
                gameList.AddToList(new Game(newId, newPrice, newName, newSales));
            }
        }

        class GameList 
        {
            Game game;
            ListEmpty listEmpty;
            public GameList(Game g, ListEmpty le)
            {
                game = g;
                listEmpty = le;
            }
            int gameListSize = 30;
            
            Random rand = new Random();

            List<Game> games = new List<Game>();

            public List<Game> CreateGameList()
            {
                if (!listEmpty.IsListEmpty())
                {
                    Console.WriteLine("Error! Cannot create a new list as list is not empty.");
                    Console.ReadLine();
                }
                else
                {
                    for (int i = 0; i < gameListSize; i++)
                    {
                        int newPrice = game.ReturnNewPrice();
                        string newName = game.ReturnNewName();
                        int newSold = game.ReturnNewSale();
                        games.Add(new Game(i, newPrice, newName, newSold));
                    }
                    listEmpty.SetListEmpty(false);
                }
                return ReturnGames();
            }

            public void AddToList(Game g)
            {
                games.Add(g);
            }

            public List<Game> ReturnGames()
            {
                return games;
            }


            public int ReturnListSize() 
            {
                return games.Count();
            }

            public void ClearList()
            {
                if (!listEmpty.IsListEmpty())
                {
                    listEmpty.SetListEmpty(true);
                    games.Clear();
                }
            }
        }

        class MergeSortAlgorithm
        {
            ListEmpty listEmpty;
            public MergeSortAlgorithm(ListEmpty le) 
            {
                listEmpty = le;
            }
            public List<Game> MergeSort(List<Game> unsorted)
            {
                if (unsorted.Count <= 1)
                    return unsorted;

                List<Game> left = new List<Game>();

                List<Game> right = new List<Game>();

                int middle = unsorted.Count / 2;
                for (int i = 0; i < middle; i++)  //Dividing the unsorted list
                {
                    left.Add(unsorted[i]);
                }
                for (int i = middle; i < unsorted.Count; i++)
                {
                    right.Add(unsorted[i]);
                }

                left = MergeSort(left);
                right = MergeSort(right);
                return Merge(left, right);
            }

            public List<Game> Merge(List<Game> left, List<Game> right)
            {
                List<Game> result = new List<Game>();

                while (left.Count > 0 || right.Count > 0)
                {
                    if (left.Count > 0 && right.Count > 0)
                    {
                        if (left[0].ReturnPrice() <= right[0].ReturnPrice())  //Comparing First two elements to see which is smaller
                        {
                            result.Add(left.First());
                            left.Remove(left.First());      //Rest of the list minus the first element
                        }
                        else
                        {
                            result.Add(right.First());
                            right.Remove(right.First());
                        }
                    }
                    else if (left.Count > 0)
                    {
                        result.Add(left.First());
                        left.Remove(left.First());
                    }
                    else if (right.Count > 0)
                    {
                        result.Add(right.First());

                        right.Remove(right.First());
                    }
                }
                return result;
            }

            public void PrintAll(List<Game> list, bool sort)
            {
                if (!sort)
                {
                    for (int i = 0; i < list.Count; i++)
                        Console.WriteLine("$" + list[i].ReturnPrice() + ": " + list[i].ReturnName());
                }
                else
                {
                    for (int i = list.Count - 1; i >= 0; i--)
                        Console.WriteLine("$" + list[i].ReturnPrice() + ": " + list[i].ReturnName());
                }
            }

            public void Print10(Game g, GameList gl)
            {
                if (listEmpty.IsListEmpty())
                {
                    Console.WriteLine("The current Game List is empty.  No games to print");
                    Console.ReadLine();
                    return;
                }
                else
                {
                    Random r = new Random();
                    List<Game> randomList = new List<Game>();
                    List<Game> currentList = gl.ReturnGames();
                    int size = gl.ReturnListSize();
                    int counter = 0;
                    while (counter < 10)
                    {
                        int index = r.Next(0, size);
                        if (!randomList.Contains(currentList[index]))
                        {
                            randomList.Add(currentList[index]);
                            counter++;
                        }
                    }
                    for (int l = 0; l < randomList.Count; l++)
                    {
                        Console.WriteLine("$" + randomList[l].ReturnPrice() + ": " + randomList[l].ReturnName());
                    }
                }
            }
        }
    }
}