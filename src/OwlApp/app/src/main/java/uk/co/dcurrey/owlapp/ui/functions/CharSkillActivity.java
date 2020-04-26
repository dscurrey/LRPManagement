package uk.co.dcurrey.owlapp.ui.functions;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.database.characterSkill.CharacterSkillEntity;
import uk.co.dcurrey.owlapp.model.repository.Repository;
import uk.co.dcurrey.owlapp.sync.NetworkMonitor;
import uk.co.dcurrey.owlapp.sync.Synchroniser;

public class CharSkillActivity extends AppCompatActivity
{
    EditText charId;
    EditText skillId;
    Button btnSave;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_char_skill);

        charId = findViewById(R.id.charSkillChar);
        skillId = findViewById(R.id.charSkillSkill);
        btnSave = findViewById(R.id.saveCharSkill);

        btnSave.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                CharacterSkillEntity charSkill = new CharacterSkillEntity();
                charSkill.CharacterId = Integer.parseInt(charId.getText().toString());
                charSkill.SkillId = Integer.parseInt(skillId.getText().toString());
                charSkill.IsSynced = false;

                saveCharSkill(charSkill);

                finish();
            }
        });
    }

    private void saveCharSkill(CharacterSkillEntity charSkill)
    {
        if (NetworkMonitor.checkNetConnectivity(getApplicationContext()))
        {
            saveApi(charSkill);
        }
        else
        {
            saveLocal(charSkill);
        }
    }

    private void saveApi(CharacterSkillEntity charSkill)
    {
        Synchroniser synchroniser = new Synchroniser();
        synchroniser.sendToAPI(getApplicationContext(), charSkill);
        charSkill.IsSynced = true;
        saveLocal(charSkill);
    }

    private void saveLocal(CharacterSkillEntity charSkill)
    {
        Repository.getInstance().getCharacterSkillRepository().insert(charSkill);
    }
}
