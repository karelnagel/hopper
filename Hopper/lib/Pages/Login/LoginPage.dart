import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:hopper/Design/Widgets.dart';
import 'package:hopper/GoogleSignin.dart';
import 'package:hopper/Pages/Register/RegisterPage.dart';

class LoginPage extends StatefulWidget {
  @override
  _LoginPageState createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  String _email='';
  String _password='';
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
              icon:Icon(Icons.email_outlined,size:20),
              onChanged: (text) {
                _email = text;
              },
              label:"Email"
            ),
            CustomTextField(

                icon:Icon(Icons.vpn_key,size:20),
              obscureText: true,
              onChanged: (text) {
                _password = text;
              },
              label:"Password"
            ),
            SizedBox(height: 20,),
            CustomButton(
              "Login",
              onPressed: () async {
                try {
                  await FirebaseAuth.instance
                      .signInWithEmailAndPassword(
                          email: _email, password: _password);
                } on FirebaseAuthException catch (e) {
                  if (e.code == 'user-not-found') {
                    error= 'No user found for that email. ';
                  } else if (e.code == 'wrong-password') {
                    error='Wrong password provided for that user.';
                  }
                }
                setState(() {

                });
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
                  Navigator.push(
                      context,
                      MaterialPageRoute(
                          builder: (context) => RegisterPage()));
                },
                child: Text("Don't have an account? Register here!")),
            TextButton(
                onPressed: () async {},
                child: Text("Forgot password"),
                style: ButtonStyle())
          ],
        ),
      ),
    );
  }
}