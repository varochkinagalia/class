using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp30
{
    class Baker
    {
        //public int id { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }

        public Baker(string name, int experience)//создаем пекаря по заданным значениям(наполняем его)
        {
            Name = name;
            Experience = experience;
        }

        public Pizza MakePizza(Order order)//приготовление пиццы
        {
            Console.WriteLine($"{order.OrderId}, Пицца готовится пекарем {Name}...");
            

            return new Pizza(order.OrderId, 1); //  пицца всегда размером 1
        }
    }
    class Courier
    {
        public string Name { get; set; }
        public int CargoCapacity { get; set; }
        public List<Pizza> Cargo { get; set; } = new List<Pizza>();//багажник,чтобы класть тута пиццу

        public Courier(string name, int cargoCapacity)
        {
            Name = name;
            CargoCapacity = cargoCapacity;
        }

        public bool IsFullyLoaded()//проверяем вместится ли пицца в багажник курьерав
        {
            int currentCargoSize = Cargo.Sum(pizza => pizza.Size);//текущий размер
            return currentCargoSize >= CargoCapacity;
        }

        public void DeliverPizza(Pizza pizza)
        {
            Cargo.Add(pizza);//курьер принимает заказ, берет пиццу
        }
    }
    class Warehouse
    {
        private int _capacity;
        //private readonly int _capacity;//вместимость
        private readonly Queue<Pizza> _pizzas = new Queue<Pizza>();//пиццы на складе есть стоят

        public bool IsEmpty => _pizzas.Count == 0;

        public Warehouse(int capacity)
        {
            _capacity = capacity;
        }

        public bool ReserveSpace(int pizzaSize)//прверяем есть ли место на складе
        {
           
            int currentSize = _pizzas.Sum(pizza => pizza.Size);//текущий размер
            //return currentSize + pizzaSize <= _capacity;
            return _capacity >= currentSize + pizzaSize;// больше ли вместимость  текущего размера склада плюс размера пиццы
            
        }

        public void WaitForSpace(int pizzaSize)
        {
            while (!ReserveSpace(pizzaSize))
            {
                // Ждем, пока появится свободное место на складе
            }
        }
        
        public void StorePizza(Pizza pizza)
        {
            _pizzas.Enqueue(pizza);//добавляем пиццу в очередь
        }
        
        public Pizza GetPizza()
        {
            return _pizzas.Dequeue();//удаляем пиццу из очереди
        }

        public bool IsFull()
        {
            int currentSize = _pizzas.Sum(pizza => pizza.Size);
            return currentSize >= _capacity;
        }
    }
    class Pizza
    {
        public int OrderId { get; set; }
        public int Size { get; set; }

        public Pizza(int orderId, int size)
        {
            OrderId = orderId;
            Size = size;
        }
    }
    
    class Order
    {
        public int OrderId { get; set; }
        public DateTime DeliveryTime { get; set; }

        public Order(int orderId, DateTime deliveryTime)
        {
            OrderId = orderId;
            DeliveryTime = deliveryTime;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество пекарей: ");
            int bakerCount = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите количество курьеров: ");
            int courierCount = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите размер склада: ");
            int warehouseCapacity = int.Parse(Console.ReadLine());


            // Создание пекарей и курьеров
            List<Baker> bakers = new List<Baker>();
            for (int i = 0; i < bakerCount; i++)
            {
                Console.WriteLine($"Введите имя пекаря {i + 1}:");
                string name = Console.ReadLine();
                Console.WriteLine($"Введите опыт работы пекаря {i + 1}:");
                int experience = int.Parse(Console.ReadLine());

                bakers.Add(new Baker(name, experience));
            }

            List<Courier> couriers = new List<Courier>();
            for (int i = 0; i < courierCount; i++)
            {
                Console.WriteLine($"Введите имя курьера {i + 1}:");
                string name = Console.ReadLine();
                Console.WriteLine($"Введите объем багажника курьера {i + 1}:");
                int cargoCapacity = int.Parse(Console.ReadLine());

                couriers.Add(new Courier(name, cargoCapacity));
            }

            // Создание склада
            Warehouse warehouse = new Warehouse(warehouseCapacity);



            // Обработка заказов
            Queue<Order> ordersQueue = new Queue<Order>();

            //создали заказ заказ поступил
            while (true)
            {

                Console.WriteLine("Введите номер заказа (для выхода введите -1):");
                int orderId = int.Parse(Console.ReadLine());

                if (orderId == -1)
                    break;

                Console.WriteLine("Введите время выполнения заказа:");
                DateTime deliveryTime = DateTime.Now.AddMinutes(int.Parse(Console.ReadLine()));

                ordersQueue.Enqueue(new Order(orderId, deliveryTime));
                Console.WriteLine("Заказ поступил");
            }

            //ищем подходящего пекаря
           
                while (ordersQueue.Count > 0)
                {
                    foreach (Baker baker in bakers)
                    {
                        if (ordersQueue.Count == 0)
                            break;

                        Order order = ordersQueue.Dequeue();//удаляем элемент из очереди из начала и записываем его в заказ
                        Console.WriteLine($"{order.OrderId}, Пекарь {baker.Name} взял заказ в обработку.");// выводим какой пекарь какой заказ взял

                    //готовим пиццу

                        Pizza pizza = baker.MakePizza(order);
                    // пицца готова, теперь нужно отправить ее на склад 
                        bool isReserved = warehouse.ReserveSpace(pizza.Size);
                        if (!isReserved)
                        {
                            Console.WriteLine($"{order.OrderId}, Склад полностью заполнен. Пекарь ждет свободное место.");
                            warehouse.WaitForSpace(pizza.Size);//ждем свободное место
                            isReserved = warehouse.ReserveSpace(pizza.Size);//проверяем опять есть ли место
                        }

                        if (isReserved)
                        {
                            warehouse.StorePizza(pizza);
                            Console.WriteLine($"{order.OrderId}, Пицца готова. Пекарь {baker.Name} передал пиццу на склад.");
                        }
                        else
                        {
                            Console.WriteLine($"{order.OrderId}, Не удалось зарезервировать место на складе.");
                        }
                    }

                foreach (Courier courier in couriers)
                {
                    if (warehouse.IsEmpty)//если склад пуст
                    {
                        Console.WriteLine("Склад пуст. Курьеры ожидают появления готовых пицц.");
                        break;
                    }

                    if (courier.IsFullyLoaded())
                    {
                        Console.WriteLine($"{courier.Name}, Багажник курьера полностью заполнен.");
                        break;
                    }

                    Pizza pizza = warehouse.GetPizza();
                    courier.DeliverPizza(pizza);
                    Console.WriteLine($"{pizza.OrderId}, Курьер {courier.Name} доставил пиццу.");
                }
            }

            // Анализ выполненных заказов и вывод рекомендаций

            // Рекомендация по увеличению количества заказов
            if (ordersQueue.Count == 0)
            {
                Console.WriteLine("Рекомендация: увеличить количество заказов");
            }

            // Рекомендация по расширению склада
            if (warehouse.IsFull())
            {
                Console.WriteLine("Рекомендация: расширить склад");
            }

            // Рекомендация по найму/увольнению пекарей
            if (bakers.Count == 0)
            {
                Console.WriteLine("Рекомендация: нанять пекарей");
            }
            else if (bakers.Count > 1)
            {
                Console.WriteLine("Рекомендация: уволить одного или нескольких пекарей");
            }

            // Рекомендация по найму/увольнению курьеров
            if (couriers.Count == 0)
            {
                Console.WriteLine("Рекомендация: нанять курьеров");
            }
            else if (couriers.Count > 1)
            {
                Console.WriteLine("Рекомендация: уволить одного или нескольких курьеров");
            }



            // Завершение программы
            Console.WriteLine("Нажмите любую клавишу, чтобы выйти...");
            Console.ReadKey();




        }

    }

}
