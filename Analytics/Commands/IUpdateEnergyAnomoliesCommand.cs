namespace Analytics.Commands
{
    public interface IUpdateEnergyAnomoliesCommand
    {
        public Task<IEnumerable<DateTime>> ExecuteAsync(IFormFileCollection file);
    }
}
