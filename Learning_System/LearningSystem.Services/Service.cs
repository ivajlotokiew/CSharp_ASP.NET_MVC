using LearningSystem.Data;

namespace LearningSystem.Services
{
    public abstract class Service
    {
        private LearningSystemContext context;

        public Service()
        {
            this.context = new LearningSystemContext();
        }

        public LearningSystemContext Context { get { return this.context; } }
    }
}
