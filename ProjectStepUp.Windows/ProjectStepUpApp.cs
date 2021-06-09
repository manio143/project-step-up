using Stride.Engine;

namespace ProjectStepUp
{
    class ProjectStepUpApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}
