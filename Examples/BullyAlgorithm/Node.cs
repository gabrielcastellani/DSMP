namespace BullyAlgorithm
{
    public sealed class Node
    {
        public int Id { get; private set; }
        public bool IsAlive { get; set; } = true;
        public bool IsCoordinator { get; set; } = false;

        private List<Node> _nodes;

        public Node(int id)
        {
            Id = id;
        }

        public void SetCluster(List<Node> nodes)
        {
            _nodes = nodes;
        }

        public void StartElection()
        {
            Console.WriteLine($"Node ID: {Id} - Iniciando a eleição");

            var nodes = _nodes.Where(node => node.Id > Id && node.IsAlive);

            if (!nodes.Any())
                BecomeCoordinator();

            foreach (var node in nodes)
            {
                Console.WriteLine($"Node {Id} enviou ELECTION para {node.Id}");
                node.ReceiveElection(Id);
            }
        }

        public void ReceiveElection(int nodeId)
        {
            Console.WriteLine($"Node {Id} recebeu ELECTION de {nodeId}");

            if (IsAlive)
            {
                Console.WriteLine($"Node {Id} respondeu OK para {nodeId}");
                StartElection();
            }
        }

        public void BecomeCoordinator()
        {
            IsCoordinator = true;
            Console.WriteLine($"Node {Id} virou o NOVO COORDENADOR");

            foreach (var node in _nodes.Where(node => node.Id != Id && node.IsAlive))
                node.ReceiveCoordinator(Id);
        }

        public void ReceiveCoordinator(int newCoordinator)
        {
            IsCoordinator = false;

            Console.WriteLine($"Node {Id} reconhece {newCoordinator} como coordenador");
        }
    }
}
