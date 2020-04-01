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

    public static final String EXTRA_REPLY_FNAME = "uk.co.dcurrey.owlapp.REPLY_FNAME";
    public static final String EXTRA_REPLY_LNAME = "uk.co.dcurrey.owlapp.REPLY_LNAME";
    private EditText fnameView;
    private EditText lnameView;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_player);

        fnameView = findViewById(R.id.newPlayFName);
        lnameView = findViewById(R.id.newPlayLName);

        final Button btn = findViewById(R.id.btn_player);
        btn.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                Intent replyIntent = new Intent();
                if (TextUtils.isEmpty(fnameView.getText()) || TextUtils.isEmpty(lnameView.getText()))
                {
                    setResult(RESULT_CANCELED, replyIntent);
                }
                else
                {
                    String fname = fnameView.getText().toString();
                    String lname = lnameView.getText().toString();

                    replyIntent.putExtra(EXTRA_REPLY_FNAME, fname);
                    replyIntent.putExtra(EXTRA_REPLY_LNAME, lname);
                    setResult(RESULT_OK, replyIntent);
                }
                finish();
            }
        });
    }
}
