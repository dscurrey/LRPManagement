package uk.co.dcurrey.owlapp.ui.skill;

import android.content.Intent;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import androidx.appcompat.app.AppCompatActivity;

import uk.co.dcurrey.owlapp.R;

public class NewSkillActivity extends AppCompatActivity
{

    public static final String EXTRA_REPLY = "uk.co.dcurrey.owlapp.REPLY";
    private EditText mEditSkillView;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_skill);

        mEditSkillView = findViewById(R.id.edit_skill);

        final Button btn = findViewById(R.id.btn_skill);
        btn.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                Intent replyIntent = new Intent();
                if (TextUtils.isEmpty(mEditSkillView.getText()))
                {
                    setResult(RESULT_CANCELED, replyIntent);
                }
                else
                {
                    String skill = mEditSkillView.getText().toString();
                    replyIntent.putExtra(EXTRA_REPLY, skill);
                    setResult(RESULT_OK, replyIntent);
                }
                finish();
            }
        });
    }
}
