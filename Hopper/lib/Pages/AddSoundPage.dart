import 'dart:io';
import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:hopper/Design/Widgets.dart';
import 'package:hopper/Pages/Home/HomePage.dart';
import 'package:hopper/main.dart';
import 'package:hopper/models/Sound.dart';

class AddSoundPage extends StatefulWidget {
  Sound sound;

  var isNew = false;
  AddSoundPage({Key key, @required this.sound, this.isNew = false})
      : super(key: key);

  @override
  _AddSoundPageState createState() => _AddSoundPageState();
}

class _AddSoundPageState extends State<AddSoundPage> {
  String error="";
  File file;
  @override
  Widget build(BuildContext context) {
    var isNew = widget.isNew;
    var sound = widget.sound;
    return CustomScaffold(
      keyboardFucksUp: true,
      child: Container(
        margin: EdgeInsets.fromLTRB(60, 60, 60, 0),
        child: ListView(
          children: <Widget>[
            CustomHeading(isNew?"Add sound":"Edit sound") ,
            error.isNotEmpty? CustomError(error) :Container(),
            CustomTextField(
                onChanged: (text) {
                  sound.name = text;
                },
                initialValue: sound.name,
                label: "Name"),
            CustomTextField(
                onChanged: (text) {
                  sound.author = text;
                },
                initialValue: sound.author,
                label: "Author"),
            CustomTextField(
                onChanged: (text) {
                  sound.video = text;
                },
                initialValue: sound.video,
                label: "Video"),
            DropdownButtonFormField(
decoration: InputDecoration(
  labelText: 'Language',
),
              value: sound.language,

              onChanged: (String newValue) {
                setState(() {
                  sound.language=newValue;
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
            SizedBox(height: 20,),
            isNew
                ? CustomOutlined(
                    file == null ? "Select file" : file.path.split('/').last,
                    onPressed: () async {
                      FilePickerResult result =
                          await FilePicker.platform.pickFiles(
                        type: FileType.custom,
                        allowedExtensions: ['mp3'],
                      );

                      if (result != null) {
                        file = File(result.files.single.path);
                        setState(() {});
                      } else {}
                    },
                  )
                : Container(),
            CustomButton(
              isNew ? 'Post' : "Put",
              onPressed: () async {
                if (sound.name.isEmpty||sound.name==null) error="You have to add sound name";
                else if (sound.author.isEmpty||sound.author==null) error="You have to add sound author";
                else if (file==null&&isNew) error="You have to add sound file";
                else {
                if (isNew) {
                  applicationUser.created.add(sound.id);
                  sound.creator = applicationUser.id;
                  await sound.uploadFile(file);
                  sound.url = await sound.getUrl();
                  await sound.post();
                  allSounds.add(sound);
                } else
                  await sound.put();
                Navigator.push(context,
                    MaterialPageRoute(builder: (context) => HomePage()));
              }
                setState(() {

                });
  }
            ),
            !isNew
                ? CustomButton(
                    'Delete',
                    onPressed: () async {
                      await sound.deleteFile();
                      await sound.delete();
                      allSounds.remove(sound);
                      Navigator.pop(context);
                    },
                  )
                : Container(),
          ],
        ),
      ),
    );
  }
}
