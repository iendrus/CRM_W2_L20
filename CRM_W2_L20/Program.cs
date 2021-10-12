using System;

namespace CRM_W2_L20
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuService menuActions = new MenuService();
            menuActions.AddNewMenu(1, "Dodaj nowego Klienta", "main");
            menuActions.AddNewMenu(2, "Znajdź Klienta", "main");
            menuActions.AddNewMenu(3, "Wyświetl listę Klientów", "main");
            menuActions.AddNewMenu(4, "Usun Klienta", "main");
            menuActions.AddNewMenu(5, "Zakończ program", "main");
            menuActions.AddNewMenu(1, "Wyszukaj wg Id Klienta", "customerFind");
            menuActions.AddNewMenu(2, "Wyszukaj wg nazwiska", "customerFind");
            menuActions.AddNewMenu(3, "Wyszukaj wg Numeru telefonu", "customerFind");
            menuActions.AddNewMenu(1, "Usuń Klienta", "customerDetails");
            menuActions.AddNewMenu(2, "Powrót do menu głównego", "customerDetails");


            CustomerService customerService = new CustomerService();

            Console.WriteLine($"Witaj w systemie CRM!\r\n");
            DisplayMenu("main");
            HandleMainManu();


            void DisplayMenu(string group, bool clearView = false)
            // wyświetlanie menu w zależności od kontekstu
            {

                if (clearView)
                {
                    Console.Clear();
                }
                Console.WriteLine($"\r\nWybierz odpowiednią akcję i naciśnij Enter:\r\n");
                foreach (var el in menuActions.GetAllItems())
                {
                    if (el.Group == group)
                    {
                        Console.WriteLine(el.Id + " - " + el.Name);
                    }
                }
            }

            int CheckChosen(string ChoosenIdMenu)
            // sprawdza, czy wybrane przez uzytkownika menu jest OK
            // jesli tak, zwraca wybrany element. W przeciwnym razie, zwraca 0.
            {
                int idMenu = 0;
                int elId = 0;
                if (int.TryParse(ChoosenIdMenu, out idMenu))
                {
                    foreach (var el in menuActions.GetAllItems())
                    {
                        if (el.Id == idMenu)
                        {
                            elId = el.Id;
                            break;
                        }
                    }
                }
                return elId;

            }

            void HandleMainManu()
            // metoda obslugująca główne Menu programu
            {
                int chosenMenu = CheckChosen(Console.ReadLine());
                Console.Clear();
                switch (chosenMenu)
                {
                    case 0:
                        {
                            Console.WriteLine($"Nie wybrano żadnej poprawnej akcji.");
                        }
                        break;
                    case 1:
                        {
                            customerService.AddNewCustomerView();
                        }
                        break;
                    case 2:
                        {
                            DisplayMenu("customerFind", true);
                            string fieldName = "";
                            switch (Int32.Parse(Console.ReadLine()))
                            {
                                case 1:
                                    fieldName = "Id";
                                    break;
                                case 2:
                                    fieldName = "Surname";
                                    break;
                                case 3:
                                    fieldName = "PhoneNumber";
                                    break;
                            }
                            Console.WriteLine("Wprowadzć wyszukiwaną wartość:");

                            if (customerService.FindCustomer(fieldName, Console.ReadLine()) == 0)
                            {
                                Console.Clear();
                                Console.WriteLine("Nie znaleziono danych.");
                                break;
                            }

                            customerService.ShowFoundCustomersList();
                            int customerId = customerService.ShowCustormerDetails();
                            if (customerId > 0)
                            {
                                DisplayMenu("customerDetails");
                                switch (Int32.Parse(Console.ReadLine()))
                                {
                                    case 1:
                                        customerService.RemoveCustomer(customerId);
                                        Console.Clear();
                                        Console.WriteLine("Usunięto Klienta.");
                                        break;
                                    case 2:
                                        break;
                                }
                            }
                        }
                        break;
                    case 3:
                        {
                            customerService.ShowAllCustomersList();
                        }
                        break;
                    case 4:
                        {
                            customerService.RemoveCustomer(customerService.removeCustomerView());
                        }
                        break;
                    case 5:
                        {
                            Environment.Exit(0);
                        }
                        break;
                }
                DisplayMenu("main");
                HandleMainManu();
            }
        }
    }
}
