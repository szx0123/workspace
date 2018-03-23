public class HeroData : CommonData {

    private BigNumber m_levelUpNeedGold;
    public BigNumber LevelUpNeedGold
    {
        get { return m_levelUpNeedGold; }
    }

    public void Init()
    {
        Level = 1;
        HeroDataCopy(HeroConfigManager.Instance.data[Level]);
    }

    public void HeroDataCopy(HeroConfig _config)
    {
        CommonDataCopyHeroData(_config);
        m_levelUpNeedGold = _config.m_Upgrade_Gold;
    }
}
