package uk.co.dcurrey.owlapp.ui.functions;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import androidx.lifecycle.ViewModel;

import java.util.HashMap;

import uk.co.dcurrey.owlapp.database.character.CharacterEntity;
import uk.co.dcurrey.owlapp.database.item.ItemEntity;
import uk.co.dcurrey.owlapp.database.player.PlayerEntity;
import uk.co.dcurrey.owlapp.database.skill.SkillEntity;
import uk.co.dcurrey.owlapp.model.repository.Repository;

public class FunctionsViewModel extends ViewModel
{
    private final MutableLiveData<String> mText;
    private final MutableLiveData<Integer> mTotalCharacters;
    private final MutableLiveData<Integer> mTotalItems;
    private final MutableLiveData<Integer> mTotalPlayers;
    private final MutableLiveData<Integer> mTotalSkills;

    public FunctionsViewModel()
    {
        mText = new MutableLiveData<>();
        mText.setValue("This is the functions page");

        mTotalCharacters = new MutableLiveData<>();
        HashMap<Integer, CharacterEntity> chars = Repository.getInstance().getCharacterRepository().get();
        mTotalCharacters.setValue(chars.size());

        mTotalItems = new MutableLiveData<>();
        HashMap<Integer, ItemEntity> items = Repository.getInstance().getItemRepository().get();
        mTotalItems.setValue(items.size());

        mTotalPlayers = new MutableLiveData<>();
        HashMap<Integer, PlayerEntity> players = Repository.getInstance().getPlayerRepository().get();
        mTotalPlayers.setValue(players.size());

        mTotalSkills = new MutableLiveData<>();
        HashMap<Integer, SkillEntity> skills = Repository.getInstance().getSkillRepository().get();
        mTotalSkills.setValue(skills.size());
    }

    public LiveData<String> getText()
    {
        return mText;
    }

    public LiveData<Integer> getCharCount()
    {
        return mTotalCharacters;
    }

    public LiveData<Integer> getItemCount()
    {
        return mTotalItems;
    }

    public LiveData<Integer> getPlayerCount()
    {
        return mTotalPlayers;
    }

    public LiveData<Integer> getSkillCount()
    {
        return mTotalSkills;
    }
}
