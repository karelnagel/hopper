import 'dart:math';

import 'package:cloud_firestore/cloud_firestore.dart';
import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:hopper/Design/Colors.dart';
import 'package:hopper/Design/Fonts.dart';
import 'package:hopper/Design/Widgets.dart';
import 'package:hopper/Pages/Home/TopBar.dart';
import 'package:hopper/main.dart';
import 'package:hopper/models/Sound.dart';
import 'package:hopper/models/User.dart';
import 'package:hopper/widgets/SoundWidget.dart';

class HomePage extends StatefulWidget {
  @override
  _HomePageState createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  Future<List<Sound>> _myString;

  String filter = "All";
  String search;
  @override
  void initState() {
    super.initState();
    _myString = getSounds();
  }

  @override
  Widget build(BuildContext context) {
    double width = MediaQuery.of(context).size.width;
    double height = MediaQuery.of(context).size.height;

    return CustomScaffold(
      child: Container(
          child: Column(
            children: [
              TopBar(),
              Container(
                height: 39,
                decoration: BoxDecoration(
                  color: Color.fromRGBO(255, 255, 255, 0.15),
                  borderRadius: BorderRadius.circular(5),
                ),
                margin: EdgeInsets.only(top: 26, bottom: 18),
                child: Row(
                  children: [
                    Container(
                      width: (width - 36) * 0.65,
                      margin: EdgeInsets.only(left: 14),
                      child: TextField(
                        textAlign: TextAlign.start,
                        onSubmitted: (text) {
                          search = text;
                          setState(() {});
                        },
                        decoration: InputDecoration(
                          hintText: "Search",
                          hintStyle: TextStyle(
                            color: colorWhite,
                          ),
                          border: InputBorder.none,
                        ),
                      ),
                    ),
                    Spacer(),
                    Container(
                      child: VerticalDivider(
                        thickness: 2,
                      ),
                    ),
                    Container(
                      margin: EdgeInsets.fromLTRB(6, 0, 6, 0),
                      child: DropdownButton(
                        value: filter,
                        iconSize: 24,
                        elevation: 16,
                        style: h2,
                        underline: Container(),
                        onChanged: (String newValue) {
                          setState(() {
                            filter = newValue;
                          });
                        },
                        items: <String>['All', 'Liked', 'Created', 'Popular']
                            .map<DropdownMenuItem<String>>((String value) {
                          return DropdownMenuItem<String>(
                            value: value,
                            child: Text(value),
                          );
                        }).toList(),
                      ),
                    ),
                  ],
                ),
              ),
              Container(
                width: width,
                height: height - 179,
                child: FutureBuilder(
                  future: _myString,
                  builder: (BuildContext context,
                      AsyncSnapshot<List<Sound>> snapshot) {
                    if (!snapshot.hasData)
                      return Center(child: CircularProgressIndicator());
                    var sounds = snapshot.data;
                    if (filter == "Liked")
                      sounds =
                          sounds.where((element) => element.liked).toList();
                    if (filter == "Created") {
                      sounds = sounds.where(
                              (element) =>
                          applicationUser.created.contains(element.id)).toList();
                    }
                    if (filter == "All") sounds.shuffle(Random(1));
                    if (filter == "Popular")
                      sounds.sort((a, b) => b.likes.compareTo(a.likes));
                    if (search != null && search != "") {
                      search = search.toLowerCase();
                      sounds = sounds
                          .where((element) =>
                              element.name.toLowerCase().contains(search) ||
                              element.author.toLowerCase().contains(search) ||
                              element.video.toLowerCase().contains(search))
                          .toList();
                    }
                    List<Widget> widgets = [];
                    if (sounds.length > 0)
                      widgets = sounds
                          .map((e) => SoundWidget(
                                sound: e,
                              ))
                          .toList();
                    else
                      widgets.add(Center(child: Text("No results!")));

                    return Container(
                        child: MediaQuery.removePadding(
                      context: context,
                      removeTop: true,
                      child: ListView(
                        children: widgets,
                      ),
                    ));
                  },
                ),
              ),
            ],
          )),
    );
  }

  Future<List<Sound>> getSounds() async {
    var user = FirebaseAuth.instance.currentUser;
    applicationUser = ApplicationUser.start(user);
    if (!await applicationUser.getData()) await applicationUser.post();

    var query = FirebaseFirestore.instance.collection("sounds").where('language',isEqualTo: applicationUser.language);

    var result = await query.get();
    allSounds = [];
    for (var element in result.docs) {
      var data = element.data();
      var sound = Sound.get(data);
      sound.liked = applicationUser.liked.contains(data['id']);
      allSounds.add(sound);
    }
    return allSounds;
  }
}

List<Sound> allSounds = [];
