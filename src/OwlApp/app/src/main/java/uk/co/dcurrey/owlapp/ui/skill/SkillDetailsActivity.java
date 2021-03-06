
package uk.co.dcurrey.owlapp.ui.skill;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.os.Bundle;
import android.widget.Button;
import android.widget.EditText;

import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.database.skill.SkillEntity;
import uk.co.dcurrey.owlapp.model.repository.Repository;

public class SkillDetailsActivity extends AppCompatActivity
{
    EditText skillName;
    EditText skillXp;
    SkillEntity skill;
    Button saveChanges;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_skill_details);
        popViews();

        int skillId = getSharedPreferences(getApplicationContext().getString(R.string.pref_skillId_key), Context.MODE_PRIVATE).getInt(getApplicationContext().getString(R.string.pref_skillId_key), 0);

        skill = Repository.getInstance().getSkillRepository().get(skillId);

        skillName.setText(skill.Name);
        skillXp.setText(""+skill.Xp);

        saveChanges.setOnClickListener(v -> {
            if (skill.Name != skillName.getText().toString() || skill.Xp != Integer.parseInt(skillXp.getText().toString()))
            {
                skill.Name = skillName.getText().toString();
                skill.Xp = Integer.parseInt(skillXp.getText().toString());
                skill.IsSynced = false;
                Repository.getInstance().getSkillRepository().update(skill);
            }
            finish();
        });
    }

    private void popViews()
    {
        skillName = findViewById(R.id.detailsSkillName);
        skillXp = findViewById(R.id.detailsSkillXp);
        saveChanges = findViewById(R.id.btnEditSkill);
    }
}
