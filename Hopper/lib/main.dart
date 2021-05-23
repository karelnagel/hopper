import 'package:firebase_auth/firebase_auth.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:flutter/material.dart';
import 'package:hopper/Design/Colors.dart';

import 'package:hopper/Pages/Home/HomePage.dart';
import 'package:hopper/Pages/Login/LoginPage.dart';
import 'package:hopper/models/User.dart';

void main() => runApp(MyApp());

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    Firebase.initializeApp();

    return MaterialApp(
      theme: ThemeData(
        brightness: Brightness.dark,
        textTheme: TextTheme(
          bodyText1: TextStyle(),
          bodyText2: TextStyle(),
        ).apply(
          bodyColor: colorWhite,
        ),
      ),
      home: FutureBuilder(
          future: Firebase.initializeApp(),
          builder: (context, snapshot) {
            if (!snapshot.hasData) return Container();
            return StreamBuilder(
                stream: FirebaseAuth.instance.authStateChanges(),
                builder: (context, snapshot) {
                  if (snapshot.connectionState != ConnectionState.active) {
                    return Center(child: CircularProgressIndicator());
                  }
                  final user = snapshot.data;
                  if (user != null) {
                    return HomePage();
                  }
                  return LoginPage();
                });
          }),
    );
  }
}

ApplicationUser applicationUser;
