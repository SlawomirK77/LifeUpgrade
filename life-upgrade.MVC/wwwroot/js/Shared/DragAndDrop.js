const MakeCardsDraggable = () => {
    let newX = 0, newY = 0, startX = 0, startY = 0;
    const cards = document.getElementsByClassName('card-draggable')

    let images = [].slice.call(cards).filter((value) =>
        value.classList.contains("card-draggable") && value.offsetWidth > 0
    )
    
    let ghostImage;
    
    for (const image of images) {
        image.addEventListener('mousedown', mouseDown)
        
        function mouseDown(e){
            startX = e.clientX
            startY = e.clientY

            document.addEventListener('mousemove', mouseMove)
            document.addEventListener('mouseup', mouseUp)

            ghostImage = createGhostImage(image);
        }

        function mouseMove(e){
            newX = startX - e.clientX
            newY = startY - e.clientY

            startX = e.clientX
            startY = e.clientY
            
            moveGhostImage(ghostImage);
            setImagesProperPositions(image);
        }

        function mouseUp(e){
            document.removeEventListener('mousemove', mouseMove)
            removeGhostImage(ghostImage);
        }
        
        function setImagesProperPositions(draggedImage) {
            let giMidX = ghostImage.getBoundingClientRect().left + (ghostImage.clientWidth / 2);
            let giMidY = ghostImage.getBoundingClientRect().top + (ghostImage.clientHeight / 2);
            
            let imagesIntersections = images.filter((image) =>{
                let pos = image.getBoundingClientRect();
                let xAxisIntersection = pos.x < giMidX && (pos.x + pos.width) > giMidX;
                let yAxisIntersection = pos.y < giMidY && (pos.y + pos.height) > giMidY;
                return xAxisIntersection && yAxisIntersection;
            });
            
            let imageToMove = imagesIntersections.find(element => element !== draggedImage);
            
            if (imageToMove) {
                swapContainersOrder(draggedImage, imageToMove);
            } else {
                let nearestGhost = getImageNearestGhostImage(giMidX, giMidY);
                swapContainersOrder(draggedImage, nearestGhost);
            }        
            
        }
        
        function swapContainersOrder(containerOne, containerTwo) {
            let parent = containerOne.parentElement;
            let c1 = containerOne.getBoundingClientRect();
            let c2 = containerTwo.getBoundingClientRect();
            
            let containerToRemove;
            let containerToInsertBefore;
            
            if (c1.x <= c2.x){
                if (c1.y > c2.y){
                    containerToRemove = containerOne;
                    containerToInsertBefore = containerTwo;
                }
                else {
                    containerToRemove = containerOne;
                    containerToInsertBefore = containerTwo.nextElementSibling;
                }
            } else {
                if (c1.y < c2.y){
                    containerToRemove = containerOne;
                    containerToInsertBefore = containerTwo.nextElementSibling;
                } else {
                    containerToRemove = containerOne;
                    containerToInsertBefore = containerTwo;
                }
            }
            
            if (containerToInsertBefore){
                parent.removeChild(containerToRemove);
                parent.insertBefore(containerToRemove, containerToInsertBefore);
            }
        }
        
        function createGhostImage(image){
            removeGhostImage(ghostImage);
            let ghost = image.cloneNode(true);
            ghost.classList.replace("card-draggable", "card-draggable-ghost");
            ghost.style.left = image.getBoundingClientRect().x + 'px';
            ghost.style.top = image.getBoundingClientRect().y + 'px';
            document.body.appendChild(ghost);
            
            return ghost;
        }
        
        function moveGhostImage(image){
            image.style.top = image.offsetTop - newY + 'px'
            image.style.left = image.offsetLeft - newX + 'px'
        }
        
        function removeGhostImage(ghost){
            if (!ghost) return; 
            document.body.removeChild(ghost);
            ghostImage = undefined;
        }
        
        function getImageNearestGhostImage(giMidX, giMidY){
            return images.map(el => {
                let pos = el.getBoundingClientRect();
                return {
                    distance: Math.hypot(pos.x + (pos.width / 2) - giMidX, pos.y + (pos.height / 2) - giMidY),
                    image: el,
                }
            }).sort((a, b) => a.distance - b.distance)
                [0].image;
        }
    }
}