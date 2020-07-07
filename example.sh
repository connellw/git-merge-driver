# This script will run a quick demo of the merge driver.
# It will cause a merge conflict in a 'version.txt' file.
# -------------------------------------------------------

# Run the mergetool-setup.sh script to configure the merge driver
./mergetool-setup.sh

# Add the my-merge-tool.sh to PATH
PATH=$PATH:`pwd`

# Clean up any previous example runs
git checkout master
git branch -D demo-branch-1
git branch -D demo-branch-2

# Update 'version.txt' on branch 1
git checkout -b demo-branch-1
echo "1.1.0" > version.txt
git add version.txt
git commit -m "Bump minor version"

# Update 'version.txt' on branch 2
git checkout master
git checkout -b demo-branch-2
echo "1.0.1" > version.txt
git add version.txt
git commit -m "Bump patch version"

# Merge the two branches, causing a conflict
git merge -m "Merged in demo-branch-1" demo-branch-1
