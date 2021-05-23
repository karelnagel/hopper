import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:hopper/Design/Widgets.dart';
import 'package:hopper/GoogleSignin.dart';
import 'package:hopper/Pages/Home/HomePage.dart';
import 'package:hopper/main.dart';
import 'package:hopper/Design/Fonts.dart';

class ProfilePage extends StatefulWidget {
  @override
  _ProfilePageState createState() => _ProfilePageState();
}

class _ProfilePageState extends State<ProfilePage> {
  String _displayName;
  @override
  Widget build(BuildContext context) {
    return CustomScaffold(
      keyboardFucksUp: true,
      child: Container(
        margin: EdgeInsets.fromLTRB(60, 60, 60, 0),
        child: ListView(children: [
          CustomHeading("Settings"),
          CustomTextField(
            onChanged: (text) {
              _displayName= text;
            },
            initialValue: applicationUser.user.displayName,
            label: "Name",
          ),
          Container(
            width:280,
            child: DropdownButtonFormField(
              isExpanded: true,
              decoration: InputDecoration(
                labelText: 'Language',
              ),

              value: applicationUser.language,

              onChanged: (String newValue) {

                setState(() {
                  applicationUser.language=newValue;
                });
              },
              items: <String>['English', 'Eesti']
                  .map<DropdownMenuItem<String>>((String value) {
                return DropdownMenuItem<String>(
                  value: value,
                  child: Text(value),
                );
              }).toList(),
            ),
          ),
          Row(
            children: [
              Text("Share sounds with link?"),
              Spacer(),
              Checkbox(
                  value: applicationUser.shareWithLink,
                  onChanged: (newValue) {
                    applicationUser.shareWithLink = newValue;
                    setState(() {});
                  }),
            ],
          ),
          SizedBox(height: 30,),
          CustomButton(
            "Save",
            onPressed: () async {
              await applicationUser.put();
              await applicationUser.user.updateProfile(displayName: _displayName);
              Navigator.of(context).popUntil((route) => route.isFirst);
              Navigator.pushReplacement(context,
                  MaterialPageRoute(builder: (context) => HomePage()));
            },
          ),
          SizedBox(height: 100,),
          TextButton(child: Text("Logout",style:h1),
            onPressed: () async {
              await signOutGoogle();
              await FirebaseAuth.instance.signOut();
              Navigator.pop(context);
            },
          ),
          SizedBox(height: 30,)
        ]),
      ),
    );
  }
}
