import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:hopper/Design/Widgets.dart';
import 'package:hopper/GoogleSignin.dart';


class RegisterPage extends StatefulWidget {
  @override
  _RegisterPageState createState() => _RegisterPageState();
}

class _RegisterPageState extends State<RegisterPage> {
  String _email='';
  String _password='';
  String _repeatPassword='';
  String error='';
  @override
  Widget build(BuildContext context) {
    return CustomScaffold(
      keyboardFucksUp: true,
      child: Container(
        padding: EdgeInsets.symmetric(horizontal:30),
        child: ListView(
          children: [
            SizedBox(height: 150,),
            Image.asset("assets/images/hopper.png",width:300,),

            SizedBox(height: 40,),
            error!='' ?CustomError(error):Container(),
            CustomTextField(
                onChanged: (text) {
                  _email = text;
                },
                label:"Email"
            ),
            CustomTextField(

                obscureText: true,
                onChanged: (text) {
                  _password = text;
                },
                label:"Password"
            ),
            CustomTextField(

                obscureText: true,
                onChanged: (text) {
                  _repeatPassword = text;
                },
                label:"Repeat password"
            ),
            SizedBox(height: 20,),
            CustomButton(
              "Register",
              onPressed: () async {
                if (_password==_repeatPassword) {
                  try {
                    await FirebaseAuth.instance
                        .createUserWithEmailAndPassword(
                        email: _email,
                        password: _password
                    );
                  } on FirebaseAuthException catch (e) {
                    if (e.code == 'weak-password') {
                      error = 'The password provided is too weak.';
                    } else if (e.code == 'email-already-in-use') {
                      error = 'The account already exists for that email.';
                    }
                  } catch (e) {
                    print(e);
                  }

                  Navigator.pop(context);
                }
                else{
                  setState(() {
                    error="Passwords don't match!";
                  });
                }
              },
            ),
            SizedBox(height: 10,),
            CustomOutlined(
              "Google",
              onPressed: () async{
                var result = await signInWithGoogle();
                if (result==null){
                  error="Can't sign in with Coogle account!";
                  setState(() {

                  });
                }
              },
              image:Image(image: AssetImage("assets/images/google_logo.png"), height: 25.0),

            ),
            TextButton(
                onPressed: () async {
                  Navigator.pop(context);
                },
                child: Text("Already have an account? Log in!")),
            
          ],
        ),
      ),
    );
  }
}

