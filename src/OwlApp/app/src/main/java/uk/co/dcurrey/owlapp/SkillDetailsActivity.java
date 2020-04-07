
package uk.co.dcurrey.owlapp;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.os.Bundle;
import android.widget.TextView;

public class SkillDetailsActivity extends AppCompatActivity
{

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_skill_details);

        int skillId = getSharedPreferences(getApplicationContext().getString(R.string.pref_skillId_key), Context.MODE_PRIVATE).getInt(getApplicationContext().getString(R.string.pref_skillId_key), 0);
        TextView textView = findViewById(R.id.textView4);
        textView.setText(textView.getText().toString()+"\n ID: "+skillId);
    }
}
