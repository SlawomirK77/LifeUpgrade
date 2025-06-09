const MakeCardsDraggable = () => {
    
    let newX = 0, newY = 0, startX = 0, startY = 0;
    const cards = document.getElementsByClassName('card-draggable')

    let images = [].slice.call(cards).filter((value) =>
        value.classList.contains("card-draggable") && value.offsetLeft > 0
    )

    const imagePositions = images.map((image) =>  ({
        image: image,
        x : image.offsetLeft,
        y : image.offsetTop,
        midX: image.offsetLeft + image.clientWidth / 2,
        midY: image.offsetTop + image.clientHeight / 2,
    }));
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
        }
        
        function setImagesProperPositions(imageOnCursor) {
            let imageOnCursorMidPosition = imageOnCursor.offsetLeft + (imageOnCursor.clientWidth / 2);
            
            let imagesAxisX = images.filter((image) =>{
                return image.offsetLeft < startX && (image.offsetLeft + image.clientWidth) > startX
            })
            // add proper axisY check
            
            let imageToMove = imagesAxisX.find(element => element !== imageOnCursor)
            
            // fix image display
            
            if (imageToMove) {
                let imageToMoveMidPosition = imageToMove.offsetLeft + imageToMove.clientWidth / 2;
                
                if (imageOnCursorMidPosition < imageToMoveMidPosition) {
                    changeImagesPositions(imageOnCursor, imageToMove);
                }

                if (imageOnCursorMidPosition > imageToMoveMidPosition) {
                    changeImagesPositions(imageOnCursor, imageToMove);
                }
                
                
                
                let properPosition = getProperPosition(imageToMove, imagePositions);
            }         
            
        }
        
        function getProperPosition(image, imagePositions) {
            let positionIndex = 0;
            imagePositions.forEach((value, index) => {
                let imageMidX = image.offsetLeft + image.clientWidth / 2
                let imageMidY = image.offsetTop + image.clientHeight / 2
                
                let xNewDifference = Math.abs(imageMidX - value.midX);
                let yNewDifference = Math.abs(imageMidY - value.midY);
                let xCurrentDifference = Math.abs(imageMidX - imagePositions[positionIndex].midX);
                let yCurrentDifference = Math.abs(imageMidY - imagePositions[positionIndex].midY);
                
                if (xNewDifference < xCurrentDifference) {
                    positionIndex = index;
                }
            })
            
            return imagePositions[positionIndex];
        }
        
        function changeImagesPositions(imageOne, imageTwo) {
            let imageOneOffsetLeft = imageTwo.offsetLeft + 'px';
            let imageOneOffsetTop = imageTwo.offsetTop + 'px';
            let imageTwoOffsetLeft = imageOne.offsetLeft + 'px';
            let imageTwoOffsetTop = imageOne.offsetTop + 'px';
            
            imageOne.style.left = imageOneOffsetLeft;
            imageOne.style.top = imageOneOffsetTop;
            imageTwo.style.left = imageTwoOffsetLeft;
            imageTwo.style.top = imageTwoOffsetTop;
            
            swapImagePosition(imageOne, imageTwo, imagePositions);
        }
        
        function swapImagePosition(imageOne, imageTwo, positionsList){
            let images = positionsList.filter(x => x.image === imageOne || x.image === imageTwo);
            images[0].x = images[0].x ^ images[1].x;
            images[1].x = images[0].x ^ images[1].x;
            images[0].x = images[0].x ^ images[1].x;
            
            images[0].y = images[0].y ^ images[1].y;
            images[1].y = images[0].y ^ images[1].y;
            images[0].y = images[0].y ^ images[1].y;
        }
        
        function createGhostImage(image){
            if(ghostImage) removeGhostImage(ghostImage);
            let ghost = image.cloneNode(true);
            ghost.classList.replace("card-draggable", "card-draggable-ghost");
            ghost.style.left = image.offsetLeft + 'px';
            ghost.style.top = image.offsetTop + 'px';
            document.body.appendChild(ghost);
            
            return ghost;
        }
        
        function moveGhostImage(image){
            image.style.top = (image.offsetTop - newY) + 'px'
            image.style.left = (image.offsetLeft - newX) + 'px'
        }
        
        function removeGhostImage(ghost){
            document.body.removeChild(ghost)
            ghostImage = undefined;
        }
    }
}