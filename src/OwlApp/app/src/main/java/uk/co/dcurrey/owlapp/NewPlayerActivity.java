package uk.co.dcurrey.owlapp;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.text.TextUtils;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

public class NewPlayerActivity extends AppCompatActivity
{

    public static final String EXTRA_REPLY = "uk.co.dcurrey.owlapp.REPLY";
    private EditText mEditPlayerView;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_player);

        mEditPlayerView = findViewById(R.id.edit_player);

        final Button btn = findViewById(R.id.btn_player);
        btn.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                Intent replyIntent = new Intent();
                if (TextUtils.isEmpty(mEditPlayerView.getText()))
                {
                    setResult(RESULT_CANCELED, replyIntent);
                }
                else
                {
                    String player = mEditPlayerView.getText().toString();
                    replyIntent.putExtra(EXTRA_REPLY, player);
                    setResult(RESULT_OK, replyIntent);
                }
                finish();
            }
        });
    }
}
