using System;
using System.Collections.Generic;

// Luokka varusteille
class Equipment
{
    public string Name { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }

    public Equipment(string name, int attack, int defense)
    {
        Name = name;
        Attack = attack;
        Defense = defense;
    }
}

// Luokka taikajuomille
class Potion
{
    public string Name { get; set; }
    public int Healing { get; set; }

    public Potion(string name, int healing)
    {
        Name = name;
        Healing = healing;
    }
}

// Luokka vihollisille
class Enemy
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }

    public Enemy(string name, int health, int attack)
    {
        Name = name;
        Health = health;
        Attack = attack;
    }
}

// Pelaajan ritari luokka
class Knight
{
    public string Name { get; set; }
    public int Health { get; private set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Gold { get; set; }

    private List<Equipment> equipmentSlots = new List<Equipment>();
    private List<Potion> potions = new List<Potion>();

    public Knight(string name)
    {
        Name = name;
        Health = 100;
        Attack = 10;
        Defense = 5;
        Gold = 0;
    }

    public void Equip(Equipment equipment)
    {
        equipmentSlots.Add(equipment);
        Attack += equipment.Attack;
        Defense += equipment.Defense;
        Console.WriteLine($"Varustettu: {equipment.Name}");
    }

    public void UsePotion(Potion potion)
    {
        if (potions.Count > 0)
        {
            Health += potion.Healing;
            Console.WriteLine($"Käytit taikajuomaa {potion.Name} ja palautit {potion.Healing} HP:tä.");
            potions.RemoveAt(0);
        }
        else
        {
            Console.WriteLine("Sinulla ei ole taikajuomia.");
        }
    }

    public void ResetHealth()
    {
        Health = 100;
    }
}

// Kauppa luokka
class Shop
{
    private List<Equipment> availableEquipment = new List<Equipment>();
    private List<Potion> availablePotions = new List<Potion>();

    public Shop()
    {
        // Alustetaan kauppa varusteilla ja taikajuomilla
        Equipment sword = new Equipment("Miekkani", 15, 0);
        Equipment shield = new Equipment("Kilpi", 0, 10);
        Equipment armor = new Equipment("Haarniska", 0, 20);

        availableEquipment.Add(sword);
        availableEquipment.Add(shield);
        availableEquipment.Add(armor);

        Potion healthPotion = new Potion("Parantava juoma", 30);
        Potion manaPotion = new Potion("Manajuoma", 20);

        availablePotions.Add(healthPotion);
        availablePotions.Add(manaPotion);
    }

    public void VisitShop(Knight player)
    {
        Console.WriteLine("Tervetuloa kauppaan!");
        Console.WriteLine("1. Osta varusteita");
        Console.WriteLine("2. Osta taikajuomia");
        Console.WriteLine("3. Poistu kaupasta");

        int shopChoice = int.Parse(Console.ReadLine());

        switch (shopChoice)
        {
            case 1:
                BuyEquipment(player);
                break;
            case 2:
                BuyPotion(player);
                break;
            case 3:
                Console.WriteLine("Poistut kaupasta.");
                break;
            default:
                Console.WriteLine("Virheellinen valinta.");
                break;
        }
    }

    private void BuyEquipment(Knight player)
    {
        Console.WriteLine("Saatavilla olevat varusteet:");
        for (int i = 0; i < availableEquipment.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {availableEquipment[i].Name} - {availableEquipment[i].Attack} ATK, {availableEquipment[i].Defense} DEF");
        }

        Console.WriteLine("Valitse varuste ostettavaksi (syötä numero) tai paina 0 poistuaksesi:");
        int equipmentChoice = int.Parse(Console.ReadLine());

        if (equipmentChoice == 0)
            return;

        if (equipmentChoice > 0 && equipmentChoice <= availableEquipment.Count)
        {
            Equipment selectedEquipment = availableEquipment[equipmentChoice - 1];
            if (player.Gold >= 20)
            {
                player.Equip(selectedEquipment);
                player.Gold -= 20;
            }
            else
            {
                Console.WriteLine("Sinulla ei ole tarpeeksi kultaa.");
            }
        }
        else
        {
            Console.WriteLine("Virheellinen valinta.");
        }
    }

    private void BuyPotion(Knight player)
    {
        Console.WriteLine("Saatavilla olevat taikajuomat:");
        for (int i = 0; i < availablePotions.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {availablePotions[i].Name} - Parantaa {availablePotions[i].Healing} HP");
        }

        Console.WriteLine("Valitse taikajuoma ostettavaksi (syötä numero) tai paina 0 poistuaksesi:");
        int potionChoice = int.Parse(Console.ReadLine());

        if (potionChoice == 0)
            return;

        if (potionChoice > 0 && potionChoice <= availablePotions.Count)
        {
            Potion selectedPotion = availablePotions[potionChoice - 1];
            if (player.Gold >= 10)
            {
                player.UsePotion(selectedPotion);
                player.Gold -= 10;
            }
            else
            {
                Console.WriteLine("Sinulla ei ole tarpeeksi kultaa.");
            }
        }
        else
        {
            Console.WriteLine("Virheellinen valinta.");
        }
    }
}

class Program
{
    static void Main()
    {
        // Alustetaan pelaajan ritari
        Knight player = new Knight("Sir Arthur");

        // Alustetaan kauppa
        Shop shop = new Shop();

        // Alustetaan viholliset
        Enemy dragon = new Enemy("Dragon", 100, 10);
        Enemy goblin = new Enemy("Goblin", 30, 5);
        Enemy skeleton = new Enemy("Skeleton", 50, 8);

        // Pelin päälooppi
        while (true)
        {
            Console.WriteLine($"Tervetuloa, {player.Name}!");
            Console.WriteLine("1. Taistele vihollista vastaan");
            Console.WriteLine("2. Vieraile kaupassa");
            Console.WriteLine("3. Poistu pelistä");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    // Valitaan vihollinen
                    Console.WriteLine("Valitse vihollinen:");
                    Console.WriteLine($"1. {dragon.Name}");
                    Console.WriteLine($"2. {goblin.Name}");
                    Console.WriteLine($"3. {skeleton.Name}");
                    int enemyChoice = int.Parse(Console.ReadLine());

                    Enemy currentEnemy = null;

                    // Asetetaan valittu vihollinen
                    switch (enemyChoice)
                    {
                        case 1:
                            currentEnemy = dragon;
                            break;
                        case 2:
                            currentEnemy = goblin;
                            break;
                        case 3:
                            currentEnemy = skeleton;
                            break;
                        default:
                            Console.WriteLine("Virheellinen valinta.");
                            break;
                    }

                    // Taistelu
                    Battle(player, currentEnemy);

                    break;
                case 2:
                    // Vieraillaan kaupassa
                    shop.VisitShop(player);
                    break;
                case 3:
                    // Poistutaan pelistä
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Virheellinen valinta.");
                    break;
            }
        }
    }

    // Taistelumetodi
    static void Battle(Knight player, Enemy enemy)
    {
        Console.WriteLine($"Taistelet vastaan {enemy.Name}!");

        while (true)
        {
            // Pelaajan vuoro
            Console.WriteLine($"{player.Name}: HP {player.Health}");
            Console.WriteLine($"{enemy.Name}: HP {enemy.Health}");
            Console.WriteLine("1. Hyökkää");
            Console.WriteLine("2. Juo taikajuoma");

            int battleChoice = int.Parse(Console.ReadLine());

            switch (battleChoice)
            {
                case 1:
                    int playerDamage = player.Attack - enemy.Defense;
                    if (playerDamage < 0)
                        playerDamage = 0;
                    enemy.Health -= playerDamage;
                    Console.WriteLine($"Hyökkäsit {playerDamage} vahinkoa!");
                    break;
                case 2:
                    player.UsePotion();
                    break;
                default:
                    Console.WriteLine("Virheellinen valinta.");
                    break;
            }

            // Tarkistetaan vihollisen tila
            if (enemy.Health <= 0)
            {
                Console.WriteLine($"Voitit taistelun! Saat 10 kultaa.");
                player.Gold += 10;
                player.ResetHealth();
                break;
            }

            // Vihollisen vuoro
            int enemyDamage = enemy.Attack - player.Defense;
            if (enemyDamage < 0)
                enemyDamage = 0;
            player.Health -= enemyDamage;
            Console.WriteLine($"{enemy.Name} hyökkäsi ja teki {enemyDamage} vahinkoa!");

            // Tarkistetaan pelaajan tila
            if (player.Health <= 0)
            {
                Console.WriteLine($"Hävisit taistelun. Palaat kauppaan.");
                player.ResetHealth();
                break;
            }
        }
    }
}