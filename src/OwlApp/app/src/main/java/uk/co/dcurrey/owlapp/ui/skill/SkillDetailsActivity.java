
package uk.co.dcurrey.owlapp.ui.skill;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import uk.co.dcurrey.owlapp.R;
import uk.co.dcurrey.owlapp.database.skill.SkillEntity;
import uk.co.dcurrey.owlapp.model.repository.Repository;

public class SkillDetailsActivity extends AppCompatActivity
{
    EditText skillName;
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

        saveChanges.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                Toast.makeText(SkillDetailsActivity.this, "DEBUG: Skill Updated", Toast.LENGTH_SHORT).show();
                finish();
            }
        });
    }

    private void popViews()
    {
        skillName = findViewById(R.id.detailsSkillName);
        saveChanges = findViewById(R.id.btnEditSkill);
    }
}
