using UnityEngine;

public class WardenBehavior : MonoBehaviour
{
    BehaviorTree tree;

    public GameObject EX;

    Node.Status treeStatus = Node.Status.RUNNING;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tree = new BehaviorTree();
        Sequence steal = new Sequence("steal something");
        Leaf goToDiamond = new Leaf("Go To Diamond", GoToDiamond);
        Leaf goToVan = new Leaf("Go To Van", GoToVan);
        Leaf dummyChild = new Leaf("Dummy Child", DummyChild);
        Leaf dummyChild1 = new Leaf("Dummy Child 1", DummyChild1);
        Selector dummy = new Selector("Dummy selector");

        dummy.AddChild(dummyChild);
        dummy.AddChild(dummyChild1);

        steal.AddChild(goToDiamond);
        steal.AddChild(goToVan);

        tree.AddChild(steal);

        tree.PrintTree();

        /*
        Node nonTelegraphed = new Node("Nontelegraphed");
        Node telegraphed = new Node("Telegraphed");
        TO-DO: add rest of nodes
        */
    }

    public Node.Status GoToDiamond()
    {
        Debug.Log("Go To Diamond");
        return Node.Status.SUCCESS;
    }

    public Node.Status GoToVan()
    {
        Debug.Log("Go To Van");
        return Node.Status.SUCCESS;
    }
    public Node.Status DummyChild()
    {
        Debug.Log("Dummy Child");
        return Node.Status.SUCCESS;
    }
    public Node.Status DummyChild1()
    {
        Debug.Log("Dummy Child 1");
        return Node.Status.SUCCESS;
    }
    // Update is called once per frame
    void Update()
    {
        if (treeStatus == Node.Status.RUNNING)
        {
            treeStatus = tree.Process();
        }
    }
}
