using System; 

public static class BinaryTreeExtensions {

  private static void PrintNode<T>(T rootValue, T childValue) {
    Console.WriteLine("  \"{0}\" -> \"{1}\"", rootValue, childValue);
  }

  private static void PrintNode<T>(T value, int empties) {
    Console.WriteLine("  empty{0} [label=\"\", style=invis];", empties);
    Console.WriteLine("  \"{0}\" -> empty{1}", value,  empties);
  }

  private static void PrintSubTree<T>(BinaryTree<T> tree, ref int empties) {

    if (tree.left == null && tree.right == null) {
      Console.WriteLine("  \"{0}\" [shape=rectangle];", tree.value);
      return;
    }

    if (tree.left != null) {
      PrintNode(tree.value, tree.left.value);
      PrintSubTree(tree.left, ref empties);
    } else  if (tree.right != null) {
      PrintNode(tree.value, empties++);
    }
    
    if (tree.right != null) {
      PrintNode(tree.value, tree.right.value);
      PrintSubTree(tree.right, ref empties);
    } else if (tree.left != null) {
      PrintNode(tree.value, empties++);
    }
  }

  public static void PrintDot<T>(this BinaryTree<T> tree) {
    Console.WriteLine("digraph G {");
    int empties = 0;
    PrintSubTree(tree, ref empties); 
    Console.WriteLine("}"); 
  }
}
