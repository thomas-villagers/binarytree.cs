using System;
using System.Collections.Generic; 

  public class BinaryTree<T> {

    public BinaryTree<T> left;
    public BinaryTree<T> right;

    public readonly T value;
    public delegate int CompareDelegate(T v1, T v2); 
    CompareDelegate compare = Comparer<T>.Default.Compare;

    public BinaryTree(T value) {
      this.value = value;
      left = null;
      right = null;
    }

    public BinaryTree(T value, CompareDelegate compare) : this(value) {
      this.compare = compare; 
    }

    public void Insert(T value) {
      if (compare(value, this.value) < 0)
      {
        if (left == null) left = new BinaryTree<T>(value, compare);
        else left.Insert(value);
      }
      else  if (right == null)
        right = new BinaryTree<T>(value, compare);
      else right.Insert(value);
    }

    public void Insert(params T[] values) {
      foreach(T value in values) Insert(value);
    }
  }
