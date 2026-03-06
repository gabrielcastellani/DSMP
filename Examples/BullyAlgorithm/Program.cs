using BullyAlgorithm;

var nodes = new List<Node>
{
    new Node(1),
    new Node(2),
    new Node(3),
    new Node(4),
    new Node(5){ IsCoordinator = true },
};

foreach (var node in nodes)
    node.SetCluster(nodes);

Console.WriteLine("Líder inicial: Node 5");

// Simula uma falha
nodes.FirstOrDefault(item => item.Id == 5).IsAlive = false;

Console.WriteLine("Node 2 detectou falha e iniciou eleição");
nodes.First(n => n.Id == 2).StartElection();
