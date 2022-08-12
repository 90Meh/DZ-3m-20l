using System;

namespace DZ_3m_20l
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            //Создание пустого корня
            NodeUser root = null;
            //Цикл заполнения дерева
            while (true)
            {
                var s = UserData();
                if (s.name == "")
                {
                    break;
                }

                if (root == null)
                {
                    root = new NodeUser
                    {
                        Name = s.name,
                        Salary = s.salary
                    };
                }
                else
                {
                    AddNode(root, new NodeUser
                    {
                        Name = s.name,
                        Salary = s.salary
                    });
                }

            }
            if (root == null)
            {
                Console.WriteLine("Таблица пуста");
                Main(args);                
            }
            //Обход дерева с выводом в консоль
            Console.WriteLine("Таблица сотрудников.");
            GoRound(root);

            //Поиск по зарплате
            var a = false;
            do
            {
                
                Console.WriteLine("Введите размер зарплаты для получения данных.");
                var salarySearch = int.TryParse(Console.ReadLine(), out int needle);
                if (salarySearch)
                {
                    var node = Find(root, needle);
                    if (node != null)
                    {
                        Console.WriteLine("Нашли сотрудника c такой зарплатой: " + node.Name);
                    }
                    else
                    {
                        Console.WriteLine("Такой сотрудник не найден.");
                    }

                }
                else
                {
                    Console.WriteLine("Введено не число");
                }

                //Запрос перезапуска
                Console.WriteLine(@"Хотите повторить поиск? (При вводе ""0"" Перезапуск программы- При вводе ""1"" Повторный запрос)");
                var question = Console.ReadLine();
                if (question == "0")
                {
                    root = null;
                    Main(args);
                }
                else if (question == "1")
                {
                    a = true;
                }
                else
                {
                    Console.WriteLine("Вредный пользователь, мне лень писать ещё один цикл или выносить всё в метод");
                    a = true;
                }

            } while (a);
        }

        //Метод для получения имени и размера зарплаты. 
        static (string name, int salary) UserData()
        {
            Console.WriteLine(@"Введите имя сотрудника. Чтобы прекратить выполнение нажмите ""Enter""");
            var name = Console.ReadLine();
            if (name == "")
            {
                return (name, 0);
            }
            Console.WriteLine("Введите размер зарплаты сотрудника.");
            var salaryB = int.TryParse(Console.ReadLine(), out int salary);
            if (salaryB)
            {
                return (name, salary);
                
            }
            else
            {
                Console.WriteLine("Число некорректно, введите ещё раз.");
                return UserData();
            }

        }


        // Метод обхода дерева (он же для сортировки)
        static void GoRound(NodeUser nodeUser)
        {
            if (nodeUser.Left != null)
            {
                GoRound(nodeUser.Left);
            }

            Console.WriteLine($"Имя сотрудника {nodeUser.Name} || Зарплата сотрудника = {nodeUser.Salary}");

            if (nodeUser.Right != null)
            {
                GoRound(nodeUser.Right);
            }
        }


        //Метод заполнения дерева значениями
        static void AddNode(NodeUser nodeUser, NodeUser toAdd)
        {
            if (toAdd.Salary < nodeUser.Salary)
            {
                // добавляем в левое поддерево
                if (nodeUser.Left == null)
                {
                    nodeUser.Left = toAdd;
                }
                else
                {
                    AddNode(nodeUser.Left, toAdd);
                }
            }
            else
            {
                // добавляем в правое поддерево
                if (nodeUser.Right == null)
                {
                    nodeUser.Right = toAdd;
                }
                else
                {
                    AddNode(nodeUser.Right, toAdd);
                }
            }
        }

        //Метод поиска по дереву
        static NodeUser Find(NodeUser nodeUser, int needle)
        {
            if (needle < nodeUser.Salary)
            {
                // ищем в левом поддереве
                if (nodeUser.Left == null)
                {
                    return null;
                }
                return Find(nodeUser.Left, needle);
            }
            else if (needle > nodeUser.Salary)
            {
                // ищем в правом поддереве
                if (nodeUser.Right == null)
                {
                    return null;
                }
                return Find(nodeUser.Right, needle);
            }
            else
            {
                return nodeUser;
            }
        }

        //Класс - узел дерева.
        class NodeUser
        {
            public string Name { get; set; }
            public int Salary { get; set; }
            public NodeUser Left { get; set; }
            public NodeUser Right { get; set; }
        }


    }
}
