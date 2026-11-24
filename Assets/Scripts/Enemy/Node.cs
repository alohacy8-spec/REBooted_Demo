using System;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Node
{
    public enum Status { SUCCESS, RUNNING, FAILURE };
    public Status status;
    public List<Node> children = new List<Node>();
    public int currentChild = 0;
    public string name;

    //empty constructor to make C# happy
    public Node() { }
    public Node(string n)
    {
        name = n;
    }

    public virtual Status Process()
    {
        return children[currentChild].Process();
    }
    public void AddChild(Node n)
    {
        children.Add(n);
    }
}
