# VBAND
Visual Basic (VB) for Android

We want to take a program that is written in VB-like language to Android Java. e.g.

#VBAND
Imports android.app.Activity
Imports android.os.Bundle

NameSpace example.app.com

	Public Class MainActivity
	
		Inherits Activity
		
		Protected Overrides Sub onCreate(Byval savedInstanceState As Bundle)
		
			MyBase.onCreate(savedInstanceState)
			
			setContentView(R.layout.activity_main)
			
		End Sub
		
	End Class
	
End NameSpace


#Android JAVA

package example.app.com

import android.app.Activity;

import android.os.Bundle;

public class MainActivity extends Activity{

	@Overrides
		
    	protected void onCreate(Bundle savedInstanceState){
    
		super.onCreate(savedInstanceState);
			
		setContentView(R.layout.activity_main);
			
	}
		
}
