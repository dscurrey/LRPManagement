package uk.co.dcurrey.owlapp.model.repository;

public enum Repository
{
    INSTANCE;

    public static Repository getInstance()
    {
        return INSTANCE;
    }

    public synchronized CharacterRepository getCharacterRepository()
    {
        return CharacterRepository.INSTANCE;
    }

    public synchronized PlayerRepository getPlayerRepository()
    {
        return PlayerRepository.INSTANCE;
    }

    public synchronized SkillRepository getSkillRepository()
    {
        return SkillRepository.INSTANCE;
    }

    public synchronized ItemRepository getItemRepository()
    {
        return ItemRepository.INSTANCE;
    }

    public synchronized BondRepository getBondRepository()
    {
        return BondRepository.INSTANCE;
    }

    public synchronized CharacterSkillRepository getCharacterSkillRepository()
    {
        return CharacterSkillRepository.INSTANCE;
    }
}
