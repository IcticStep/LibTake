using System.Threading.Tasks;

namespace Code.Runtime.Infrastructure.States.Api
{
    public interface IState : IExitableState
    {
        public void Start();
    }
}