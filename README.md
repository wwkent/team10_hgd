This is the main repository for our game for HGD team 10.

10/10 would recommend using GitBash
Also some of the forking stuff still needs to be tested soo bear(?) with me here.

To setup the project:
Fork this repo
Clone the forked repository into whatever you use for your Unity Project folder
Add the remotes for your fork.
- git remote add (your_name) https://github.com/(your git username)/team10_hgd.git

Open Unity
Open the folder that you cloned
- There you go you should have everything setup
- Unity might have to create somethings

Development Workflow
1. Find task to do
2. Create an issue for the task (Title, Short Description, Assign it to yourself)
3. Make sure you are on master on your machine. (git checkout master)
4. Make sure master is up to date. (git fetch origin -> git merge origin/master)
5. Create a branch for your issue. (git checkout -b (name of issue))
6. Do work...
7. When done...
8. git add -A
9. git commit -m "(insert message here)"
10. git push origin master

IMPORTANT: Try to make sure that 2 people are not working on the same thing at the same time
