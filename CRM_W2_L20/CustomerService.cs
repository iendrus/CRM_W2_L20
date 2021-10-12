using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_W2_L20
{
    public class CustomerService
    {
        public List<Customer> customers { get; set; } = new List<Customer>();


        public int AddNewCustomer(int id, string name, string surname, long phoneNumber)
        // --- dodanie nowego Klienta
        {
            Customer customer = new Customer() { Id = id, Name = name, Surname = surname, PhoneNumber = phoneNumber };
            customers.Add(customer);
            return customer.Id;
        }


        public void AddNewCustomerView()
        {
            bool correct = false;
            int id = 0;
            long phoneNumber = 0;
            string prompt = "Podaj Id Klienta:";
            Console.WriteLine("Dodawanie nowego Klienta:\r\n");
            do
            {
                // walidacje
                // 1. sprawdź, czy id jest liczbą całkowitą
                Console.WriteLine(prompt);
                correct = Int32.TryParse(Console.ReadLine(), out id);
                if (!correct)
                {
                    prompt = $"Id klienta musi byc liczbą\r\nPodaj ponownie Id.";
                }
                else
                {
                    // 2. sprawdź, czy większe od zera
                    if (id <= 0)
                    {
                        Console.WriteLine($"Id musi być większe od zera.");
                        correct = false;
                    }
                    else
                    {
                        // 3. sprawdź, czy podane id już nie istnieje
                        if (FindCustomer("Id", id.ToString()) > 0)
                        {
                            Console.WriteLine($"Podane Id: {id} już istnieje.\r\nNie można ponownie go użyć.");
                            correct = false;
                        }
                    }
                }
            }
            while (!correct);
            Console.WriteLine("Podaj imię Klienta:");
            string name = Console.ReadLine();
            Console.WriteLine("Podaj nazwisko Klienta:");
            string surname = Console.ReadLine();
            prompt = "Podaj numer telefonu Klienta:";
            do
            {
                Console.WriteLine(prompt);
                correct = Int64.TryParse(Console.ReadLine(), out phoneNumber);
                if (!correct)
                {
                    prompt = $"Numer telefonu ma nie poprawny format!\r\nSpróbuj ponownie.";
                }
            }
            while (correct == false);

            AddNewCustomer(id, name, surname, phoneNumber);

            Console.Clear();
            Console.WriteLine($"Zapisano poprawnie dane Klienta Id: {id}\r\n");
        }

        public List<Customer> foundCustomers { get; set; }


        public int FindCustomer(string findField, string findValue)
        {
            // wyszukuje Klientów według różnych kryteriow
            // zwarca ilość znalezionych klientów
            // dodaje znalezionych klientów do: foundCustomers
            foundCustomers = new List<Customer>();
            foreach (var el in customers)
            {
                switch (findField)
                {
                    case "Id":
                        if (Int32.Parse(findValue) == el.Id)
                        {
                            foundCustomers.Add(el);
                        }
                        break;
                    case "Surname":
                        if (findValue.ToLower() == el.Surname.ToLower())
                        {
                            foundCustomers.Add(el);
                        }
                        break;
                    case "PhoneNumber":
                        if (Int64.Parse(findValue) == el.PhoneNumber)
                        {
                            foundCustomers.Add(el);
                        }
                        break;
                }
            }
            return foundCustomers.Count();
        }

        public void ShowAllCustomersList()
        {
            Console.WriteLine($"===== Lista Klientów ===== \r\n\r\nId | Imię i nazwisko | Numer tel.");
            foreach (var el in customers)
            {
                Console.WriteLine(el.Id + " | " + el.Name + " " + el.Surname + " | " + el.PhoneNumber);
            }
        }

        public void ShowFoundCustomersList()
        {
            Console.Clear();
            Console.WriteLine($"===== Lista Klientów spełniającyh kryteria ({foundCustomers.Count}) =====  " +
                $"\r\n\r\nId | Imię i nazwisko | Numer tel.");
            foreach (var el in foundCustomers)
            {
                Console.WriteLine(el.Id + " | " + el.Name + " " + el.Surname + " | " + el.PhoneNumber);
            }
        }

        public int ShowCustormerDetails()
        {
            int typedId;
            int customerId = 0;
            bool correct;

            Console.WriteLine($"\r\nWprowadź Id Klienta, aby wyświetlić szczegóły:\r\n");
            do
            {
                correct = Int32.TryParse(Console.ReadLine(), out typedId);
            }
            while (!correct);
            Console.Clear();
            string customerInfo = "";

            foreach (var el in customers)
            {
                if (typedId == el.Id)
                {
                    customerInfo = $"Karta Klienta Id: {el.Id}\r\n\r\n";
                    customerInfo = $"{ customerInfo}Id klienta: {el.Id}\r\n";
                    customerInfo = $"{ customerInfo}Imię: {el.Name}\r\n";
                    customerInfo = $"{ customerInfo}Nazwisko: {el.Surname}\r\n";
                    customerInfo = $"{ customerInfo}Telefon: {el.PhoneNumber}\r\n";
                    customerId = el.Id;
                    break;
                }
            }
            if (customerId == 0)
            {
                customerInfo = "Brak danych do wyświetlenia.";
            }
            Console.WriteLine(customerInfo);
            return customerId;

        }
        public int removeCustomerView()
        {
            bool validOK;
            int customerId;
            do
            {
                Console.Clear();
                Console.WriteLine("Podaj Id Klienta, którego chcesz usunąć");
                validOK = Int32.TryParse(Console.ReadLine(), out customerId);
            }
            while (!validOK);
            return customerId;
        }

        public void RemoveCustomer(int customerId)
        {
            {
                foreach (var el in customers)
                {
                    if (customerId == el.Id)
                    {
                        customers.Remove(el);
                        break;
                    }
                }
            }
        }
    }
}
