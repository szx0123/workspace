public class ConfigDataManager : SingleInstance<ConfigDataManager> {
	public void LoadConfig() {
		AcitveCopyMapConfigManager.Instance.Init("AcitveCopyMapConfig");
		HeroConfigManager.Instance.Init("HeroConfig");
	}
}