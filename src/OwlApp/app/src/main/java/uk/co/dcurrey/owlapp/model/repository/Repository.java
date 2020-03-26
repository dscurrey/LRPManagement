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
}
