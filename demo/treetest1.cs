using System; 

class TreeTest {

  public static void Main()
  {
    var root = new BinaryTree<int>(5); 
    root.Insert(3);
    root.Insert(7);
    root.Insert(1);
    root.Insert(4);
    root.Insert(6);
    root.Insert(2);  
    Console.WriteLine(root);

    var floattree = new BinaryTree<float>(3.14f); 
    floattree.Insert(0.99f, 2.34f, 3.1415f);
    Console.WriteLine(floattree);

    var lannisters = new BinaryTree<string>("Tywin");
    lannisters.Insert("Cersei","Tyrion","Joffrey");
    lannisters.Insert("Tommen");
    lannisters.Insert("Myrcella");
    lannisters.Insert("Jamie");
    Console.WriteLine(lannisters);
  }
}
