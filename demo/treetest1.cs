using System; 

class TreeTest {

  public static void Main()
  {
    var inttree = new BinaryTree<int>(5); 
    inttree.Insert(3);
    inttree.Insert(7);
    inttree.Insert(1);
    inttree.Insert(4);
    inttree.Insert(6);
    inttree.Insert(2);  
    foreach (var i in inttree.ToList()) 
      Console.Write(i + " ");
    Console.WriteLine();
    foreach (var i in inttree.ToList(inttree.Postorder())) 
      Console.Write(i + " ");
    Console.WriteLine();
    foreach (var i in inttree.ToList(inttree.Inorder())) 
      Console.Write(i + " ");
    
    Console.WriteLine();
    var floattree = new BinaryTree<float>(3.14f); 
    floattree.Insert(0.99f, 2.34f, 3.1415f);
    foreach (var f in floattree.ToList()) 
      Console.Write(f + " ");

    var lannisters = new BinaryTree<string>("Tywin");
    lannisters.Insert("Cersei","Tyrion","Joffrey");
    lannisters.Insert("Tommen");
    lannisters.Insert("Myrcella");
    lannisters.Insert("Jamie");
    Console.WriteLine();
    foreach (var s in lannisters.ToList()) 
      Console.Write(s + " ");
    Console.WriteLine();
    foreach (var s in lannisters.ToList(lannisters.Postorder()))
      Console.Write(s + " ");
  }
}
