class TreeTest {
  public static void Main() {
    var root = new BinaryTree<int>(5); 
    root.Insert(3,7,1,4,6,2);
    root.PrintDot(); 
  }
}
