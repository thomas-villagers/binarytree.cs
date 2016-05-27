using System; 

class TreeTest {

  public static void Main() {
    var lannisters = new BinaryTree<string>("Tywin");
    lannisters.Insert("Cersei");
    lannisters.Insert("Tyrion");
    lannisters.Insert("Joffrey");
    lannisters.Insert("Tommen");
    lannisters.Insert("Myrcella");
    lannisters.Insert("Jamie");
    lannisters.PrintDot();
  }
}
