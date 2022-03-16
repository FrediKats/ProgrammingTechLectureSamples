using System.Reflection.Emit;
using System.Reflection;
using System.Runtime.Serialization;
using static System.Console;

namespace Lecture04;

public interface IShopItem
{
    public int GetPrice();
}

public class Item
{
    public string Name { get; set; }
    public int Price { get; set; }
}

public static class Database
{
    public static List<Item> GetItems()
    {
        return new List<Item>()
        {
            new Item() { Name = "Surface", Price = 1 },
            new Item() { Name = "Samsung", Price = 2 },
        };
    }
}

public class StillShopBut
{
    public static void Show()
    {
        List<Item> items = Database.GetItems();

        var stillShopBut = new StillShopBut();
        List<IShopItem> shopItems = stillShopBut.CreateShopItems(items);
        foreach (IShopItem shopItem in shopItems)
        {
            WriteLine($"{shopItem.GetType()} : {shopItem.GetPrice()}");
        }
    }

    public List<IShopItem> CreateShopItems(List<Item> items)
    {
        List<IShopItem> shopItems = new List<IShopItem>();

        AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();
        AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
        ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(nameof(UnderstoodTheIl));

        foreach (Item item in items)
        {
            TypeBuilder typeBuilder = moduleBuilder.DefineType(
                item.Name,
                TypeAttributes.Public,
                null,
                new[] { typeof(IShopItem) });

            typeBuilder.DefineDefaultConstructor(
                MethodAttributes.Private);

            MethodAttributes methodAttributes =
                MethodAttributes.Public
                | MethodAttributes.Final
                | MethodAttributes.Virtual;

            MethodBuilder methodBuilder = typeBuilder.DefineMethod(
                nameof(IShopItem.GetPrice),
                methodAttributes,
                typeof(int), Type.EmptyTypes);

            ILGenerator ilGen = methodBuilder.GetILGenerator();

            ilGen.Emit(OpCodes.Ldc_I4, item.Price);
            ilGen.Emit(OpCodes.Ret);

            Type? newType = typeBuilder.CreateType();
            object? createTypeValue = FormatterServices
                .GetUninitializedObject(newType);
            shopItems.Add((IShopItem)createTypeValue);
        }

        return shopItems;
    }
}